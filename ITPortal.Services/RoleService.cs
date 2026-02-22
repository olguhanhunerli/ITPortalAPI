using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.RoleDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleDTO> CreateAsync(CreateRoleDTO dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Role name boş olamaz.");
            var name = dto.Name.Trim();
            if (await _roleRepository.ExistsRoleByNameAsync(name))
                throw new InvalidOperationException("Bu role adı zaten mevcut.");
            var roleEntity = new Role
            {
                Name = name,
                Description = dto.Description?.Trim(),
                CreatedAt = DateTime.UtcNow
            };
            await _roleRepository.AddAsync(roleEntity);
            await _roleRepository.SaveChangesAsync();
            return _mapper.Map<RoleDTO>(roleEntity);

        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if(role == null)
                return false; 
            _roleRepository.Remove(role);
            await _roleRepository.SaveChangesAsync();

            return true;
        }

        public async Task<RoleDTO?> GetByIdAsync(ulong id)
        {
            var roleEntity = await _roleRepository.GetByIdAsync(id);
            if (roleEntity == null)
                return null;
            return _mapper.Map<RoleDTO>(roleEntity);
        }

        public async Task<List<RoleLookupDTO>> GetLookUpAsync(string? search, int take)
        {
            return await _roleRepository.GetRoleLookUpAsync(search, take);
        }

        public async Task<PagedResultDTO<RoleMiniDTO>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var paged = await _roleRepository.GetRoleWithPaginationAsync(pageNumber, pageSize);
            var items = _mapper.Map<List<RoleMiniDTO>>(paged.Items);

            return new PagedResultDTO<RoleMiniDTO>
            {
                TotalCount = paged.TotalCount,
                Page = paged.Page,
                PageSize = paged.PageSize,
                Items = items
            };
        }

        public async Task<RoleDTO> UpdateAsync(ulong id, UpdateRoleDTO dto)
        {
            var roleEntity = await _roleRepository.GetByIdAsync(id);
            if (roleEntity == null)
                throw new Exception("Role not found");
            var name = dto.Name?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Role name boş olamaz.");

            if (await _roleRepository.ExistsRoleByNameAsync(name, excludeId: id))
                throw new InvalidOperationException("Bu role adı zaten mevcut.");

            roleEntity.Name = name;
            roleEntity.Description = dto.Description?.Trim();

            _roleRepository.Update(roleEntity);
            await _roleRepository.SaveChangesAsync();
            return _mapper.Map<RoleDTO>(roleEntity);
        }
    }
}
