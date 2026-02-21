using AutoMapper;
using ITPortal.Business.Context;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.LookupDTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class LookupService : ILookupService
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public LookupService(ILookupRepository lookupRepository, IMapper mapper, AppDbContext context)
        {
            _lookupRepository = lookupRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<LookupDTO> CreateAsync(CreateLookupDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if(string.IsNullOrWhiteSpace(dto.LookupTypeCode)) throw new ArgumentException("LookupTypeCode is required.", nameof(dto.LookupTypeCode));
            if(string.IsNullOrWhiteSpace(dto.Code)) throw new ArgumentException("Code is required.", nameof(dto.Code));
            if(string.IsNullOrWhiteSpace(dto.NameTr)) throw new ArgumentException("NameTr is required.", nameof(dto.NameTr));
            if(string.IsNullOrWhiteSpace(dto.NameEn)) throw new ArgumentException("NameEn is required.", nameof(dto.NameEn));

            dto.LookupTypeCode = dto.LookupTypeCode.Trim();
            dto.Code = dto.Code.Trim();

            var type = await _context.LookupTypes.FirstOrDefaultAsync(lt => lt.Code == dto.LookupTypeCode);
            if (type == null)
                throw new ArgumentException($"LookupType bulunamadı: {dto.LookupTypeCode}");
            
            var exists = await _lookupRepository.ExistsAsync(dto.LookupTypeCode, dto.Code);
            if(exists)
                throw new ArgumentException($"Bu kod zaten mevcut: {dto.Code} (LookupType: {dto.LookupTypeCode})");

            var entity = new Lookup
            {
                LookupTypeId = type.Id,
                Code = dto.Code,
                NameTr = dto.NameTr,
                NameEn = dto.NameEn,
                DescriptionTr = dto.DescriptionTr,
                DescriptionEn = dto.DescriptionEn,
                SortOrder = dto.SortOrder,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _lookupRepository.AddAsync(entity);
            await _lookupRepository.SaveChangesAsync();

            var created = await _context.Lookups.Include(l => l.LookupType).FirstAsync(l => l.Id == entity.Id);

            return _mapper.Map<LookupDTO>(created);
        }

        public async Task<bool> DeleteAsync(ulong id)
        {
            var entity = await _context.Lookups.FirstOrDefaultAsync(l => l.Id == id && l.DeletedAt == null);
            if (entity == null)
                return false;
            _lookupRepository.Remove(entity);
            await _lookupRepository.SaveChangesAsync();
            return true;
        }

        public async Task<LookupDTO?> GetByIdAsync(ulong id)
        {
            var entity = await _context.Lookups.Include(l => l.LookupType).FirstOrDefaultAsync(l => l.Id == id && l.DeletedAt == null);
            if (entity == null)
                return null;
            return _mapper.Map<LookupDTO>(entity);
        }

        public async Task<List<LookupLookupDTO>> GetLookupsByTypeCodeAsync(string typeCode, string? search, int take)
        {
            var types = await _lookupRepository.GetLookupsByTypeCodeAsync(typeCode, search, take);
            return _mapper.Map<List<LookupLookupDTO>>(types);
        }

        public async Task<List<LookupTypeLookupDTO>> GetLookupTypesAsync(string? search, int take)
        {
           var list = await _lookupRepository.GetLookupTypesAsync(search, take);
              return _mapper.Map<List<LookupTypeLookupDTO>>(list);
        }

        public async Task<LookupDTO> UpdateAsync(ulong id, UpdateLookupDTO dto)
        {
            if(dto == null) throw new ArgumentNullException(nameof(dto));
            if(string.IsNullOrWhiteSpace(dto.Code)) throw new ArgumentException("Code is required.", nameof(dto.Code));
            if(string.IsNullOrWhiteSpace(dto.NameTr)) throw new ArgumentException("NameTr is required.", nameof(dto.NameTr));
            if(string.IsNullOrWhiteSpace(dto.NameEn)) throw new ArgumentException("NameEn is required.", nameof(dto.NameEn));

            dto.Code = dto.Code.Trim();

            var entity = await _context.Lookups.Include(l => l.LookupType).FirstOrDefaultAsync(l => l.Id == id && l.DeletedAt == null);
            if (entity == null)
                throw new ArgumentException($"Lookup bulunamadı: {id}");

            var typeCode = entity.LookupType.Code;
            if(string.IsNullOrWhiteSpace(typeCode))
                throw new ArgumentException($"LookupTypeCode is missing for Lookup ID: {id}");

            var exists = await _lookupRepository.ExistsAsync(typeCode, dto.Code, id);
            if(exists)
                throw new ArgumentException($"Bu kod zaten mevcut: {dto.Code} (LookupType: {typeCode})");
            entity.Code = dto.Code;
            entity.NameTr = dto.NameTr;
            entity.NameEn = dto.NameEn;
            entity.DescriptionTr = dto.DescriptionTr;
            entity.DescriptionEn = dto.DescriptionEn;
            entity.SortOrder = dto.SortOrder;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _lookupRepository.Update(entity);
            await _lookupRepository.SaveChangesAsync();
            return _mapper.Map<LookupDTO>(entity);
        }
    }
}
