using AutoMapper;
using ITPortal.Business.Context;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.TicketCategoryDTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class TicketCategoryService : ITicketCategoryService
    {
        private readonly ITicketCategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;

        public TicketCategoryService(ITicketCategoryRepository repository, IMapper mapper, AppDbContext dbContext)
        {
            _repository = repository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<TicketCategoryDTO> CreateAsync(CreateTicketCategoryDTO dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Code))
                throw new ArgumentException("Code is required");

            if(await _repository.ExistsCategoryCodeAsync(dto.Code))
                throw new ArgumentException("Code already exists");

            if(dto.ParentId.HasValue)
            {
                var parentExists = await _dbContext.Set<TicketCategory>()
                    .AnyAsync(x => x.Id == dto.ParentId.Value && x.DeletedAt == null);
                if (!parentExists)
                    throw new InvalidOperationException("Parent bulunamadı.");
            }
            if (dto.DefaultTeamId.HasValue)
            {
                var teamExists = await _dbContext.Set<Team>()
                    .AnyAsync(x => x.Id == dto.DefaultTeamId.Value);
                if (!teamExists)
                    throw new InvalidOperationException("Default team bulunamadı.");
            }

            if(dto.RequiresApproval && !dto.ApprovalTypeId.HasValue)
                throw new ArgumentException("ApprovalTypeId is required when RequiresApproval is true");
            if(dto.ApprovalTypeId.HasValue)
            {
                var lookupOk = await _dbContext.Lookups.AnyAsync(x => x.Id == dto.ApprovalTypeId.Value && x.DeletedAt == null);
                if (!lookupOk)
                    throw new InvalidOperationException("ApprovalType lookup bulunamadı.");
            }

            var entity = _mapper.Map<TicketCategory>(dto);
            entity.Code = dto.Code;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            var created = await _repository.GetByIdWithParentTeamAsync(entity.Id);
            return _mapper.Map<TicketCategoryDTO>(created);
        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var entity = await _dbContext.Set<TicketCategory>().FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
            if (entity == null) return false;

            var hasChildren = await _dbContext.Set<TicketCategory>().AnyAsync(x => x.ParentId == id && x.DeletedAt == null);  
            if(hasChildren)
                throw new InvalidOperationException("Bu kategorinin alt kategorileri var, önce onları silmelisiniz.");

            _repository.Remove(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<TicketCategoryDTO?> GetByIdAsync(ulong id)
        {
            var entity = await _repository.GetByIdWithParentTeamAsync(id);
            return entity == null ? null : _mapper.Map<TicketCategoryDTO>(entity);
        }

        public Task<List<TicketCategoryLookupDTO>> GetLookupAsync(string? search, int take, bool activeOnly)
        {
           return _repository.GetCategoryLookUpAsync(search, take, activeOnly);
        }

        public async Task<List<TicketCategoryTreeDTO>> GetTreeAsync(bool activeOnly)
        {
            var list = await _repository.GetAllForTreeAsync(activeOnly);

            var byParent = list.GroupBy(x => x.ParentId).ToDictionary(g => g.Key, g => g.ToList());
            List<TicketCategoryTreeDTO> Build(ulong? parentId)
            {
                if (!byParent.TryGetValue(parentId, out var children))
                    return new();
                return children.OrderBy(x => x.Code).Select (x => new TicketCategoryTreeDTO
                {
                    Id = x.Id,
                    Code = x.Code,
                    NameTr = x.NameTr,
                    NameEn = x.NameEn,
                    IsActive = x.IsActive,
                    Children = Build(x.Id)
                }).ToList();
            }
            return Build(null);
        }

        public async Task<TicketCategoryDTO> UpdateAsync(ulong id, UpdateTicketCategoryDTO dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Code))
                throw new ArgumentException("Code is required");
            var entity = await _dbContext.Set<TicketCategory>().FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);
            if (entity == null)
                throw new InvalidOperationException("Kategori bulunamadı.");

            if(await _repository.ExistsCategoryCodeAsync(dto.Code, id))
                throw new ArgumentException("Code already exists");
            if(dto.ParentId == id)
                throw new InvalidOperationException("Bir kategori kendi kendinin parentı olamaz.");

            if (dto.ParentId.HasValue)
            {
                var parentExists = await _dbContext.Set<TicketCategory>()
                    .AnyAsync(x => x.Id == dto.ParentId.Value && x.DeletedAt == null);
                if (!parentExists)
                    throw new InvalidOperationException("Parent bulunamadı.");
            }

            if(dto.DefaultTeamId.HasValue)
                {
                var teamExists = await _dbContext.Set<Team>()
                    .AnyAsync(x => x.Id == dto.DefaultTeamId.Value);
                if (!teamExists)
                    throw new InvalidOperationException("Default team bulunamadı.");
            }

            if(dto.RequiresApproval && !dto.ApprovalTypeId.HasValue)
                throw new ArgumentException("ApprovalTypeId is required when RequiresApproval is true");

            if(dto.ApprovalTypeId.HasValue)
            {
                var lookupOk = await _dbContext.Lookups.AnyAsync(x => x.Id == dto.ApprovalTypeId.Value && x.DeletedAt == null);
                if (!lookupOk)
                    throw new InvalidOperationException("ApprovalType lookup bulunamadı.");
            }

            entity.Code = dto.Code;
            entity.NameTr = dto.NameTr ?? entity.NameTr;
            entity.NameEn = dto.NameEn ?? entity.NameEn;
            entity.ParentId = dto.ParentId ?? entity.ParentId;
            entity.DefaultTeamId = dto.DefaultTeamId ?? entity.DefaultTeamId;
            entity.RequiresApproval = dto.RequiresApproval;
            entity.ApprovalTypeId = dto.ApprovalTypeId ?? entity.ApprovalTypeId;
            entity.FormSchemaJson = dto.FormSchemaJson ?? entity.FormSchemaJson;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            var updated = await _repository.GetByIdWithParentTeamAsync(entity.Id);
            return _mapper.Map<TicketCategoryDTO>(updated);

        }
    }
}
