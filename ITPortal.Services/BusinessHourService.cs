using AutoMapper;
using ITPortal.Business.Repository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.BusinessHourDTOs;
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
    public class BusinessHourService : IBusinessHourService
    {
        private readonly IBusinessHourRepository _businessHourRepository;
        private readonly IBusinessHourRuleRepository _ruleRepository;
        private readonly IMapper _mapper;

        public BusinessHourService(IBusinessHourRepository businessHourRepository, IBusinessHourRuleRepository ruleRepository, IMapper mapper)
        {
            _businessHourRepository = businessHourRepository;
            _ruleRepository = ruleRepository;
            _mapper = mapper;
        }

        public async Task<BusinessHoursDetailDTO> CreateBusinessHourAsync(CreateBusinessHoursDTO businessHourCreateDTO)
        {
            Validate(businessHourCreateDTO);

            var businessHour = new BusinessHours
            {
                Name = businessHourCreateDTO.Name,
                TimeZone = businessHourCreateDTO.TimeZone,
                Is24x7 = businessHourCreateDTO.Is24x7,
                CreatedAt = DateTime.UtcNow
            };
            await _businessHourRepository.AddAsync(businessHour);
            await _businessHourRepository.SaveChangesAsync();

            if (!businessHourCreateDTO.Is24x7)
            {
                var rules = businessHourCreateDTO.Rules.Select(r => new BusinessHoursRule
                {
                    BusinessHoursId = businessHour.Id,
                    DayOfWeek = (BusinessDay)r.DayOfWeek,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    
                }).ToList();
                await _ruleRepository.AddRangeAsync(rules);
                await _ruleRepository.SaveChangesAsync();
            }
            var created = await _businessHourRepository.GetBusinessHourByIdAsync(businessHour.Id);
            return _mapper.Map<BusinessHoursDetailDTO>(created);
        }

        public async Task<BusinessHoursDetailDTO> GetBusinessHourByIdAsync(ulong id)
        {
            var businessHour = await _businessHourRepository.GetBusinessHourByIdAsync(id);
            if (businessHour == null)
            {
                return null;
            }
            return _mapper.Map<BusinessHoursDetailDTO>(businessHour);
        }

        public async Task<PagedResultDTO<BusinessHoursMiniDTO>> GetBusinessHoursAsync(int pageNumber, int pageSize)
        {
            var paged = await _businessHourRepository.GetBusinessHoursAsync(pageNumber, pageSize);
            var items = _mapper.Map<List<BusinessHoursMiniDTO>>(paged.Items);

            return new PagedResultDTO<BusinessHoursMiniDTO>
            {
                TotalCount = paged.TotalCount,
                Page = paged.Page,
                PageSize = paged.PageSize,
                Items = items
            };
        }

        public async Task<List<BusinessHoursLookupDTO>> GetBusinessHoursLookupAsync(string? search, int take)
        {
            return await _businessHourRepository.GetBusinessHourLookupAsync(search, take);
        }

        public async Task<BusinessHoursDetailDTO> UpdateBusinessHourAsync(ulong id, UpdateBusinessHoursDTO businessHourUpdateDTO)
        {
            Validate(businessHourUpdateDTO);
            var entity = await _businessHourRepository.GetBusinessHourByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("BusinessHour bulunamadı.");
            }
            entity.Name = businessHourUpdateDTO.Name.Trim();
            entity.TimeZone = businessHourUpdateDTO.TimeZone;
            entity.Is24x7 = businessHourUpdateDTO.Is24x7;

            _businessHourRepository.Update(entity);
            await _businessHourRepository.SaveChangesAsync();

            await _ruleRepository.DeleteByBusinessHoursIdAsync(id);

            if (!businessHourUpdateDTO.Is24x7)
            {
                var rules = businessHourUpdateDTO.Rules.Select(r => new BusinessHoursRule
                {
                    BusinessHoursId = entity.Id,
                    DayOfWeek = (BusinessDay)r.DayOfWeek,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime
                }).ToList();
                await _ruleRepository.AddRangeAsync(rules);
                await _ruleRepository.SaveChangesAsync();
            }
            var updated = await _businessHourRepository.GetBusinessHourByIdAsync(entity.Id);
            return _mapper.Map<BusinessHoursDetailDTO>(updated);
        }
        private static void Validate(CreateBusinessHoursDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new Exception("Name zorunlu");

            if (string.IsNullOrWhiteSpace(dto.TimeZone))
                throw new Exception("TimeZone zorunlu");

            if (!dto.Is24x7)
            {
                if (dto.Rules == null || dto.Rules.Count == 0)
                    throw new Exception("Is24x7 false iken en az 1 rule girilmelidir.");

                foreach (var r in dto.Rules)
                {
                    if (r.StartTime >= r.EndTime)
                        throw new Exception("StartTime EndTime'dan küçük olmalıdır.");

                    if ((int)r.DayOfWeek > 6)
                        throw new Exception("DayOfWeek 0-6 aralığında olmalıdır.");

                }

                foreach (var grp in dto.Rules.GroupBy(x => x.DayOfWeek))
                {
                    var ordered = grp.OrderBy(x => x.StartTime).ToList();
                    for (int i = 1; i < ordered.Count; i++)
                    {
                        if (ordered[i].StartTime < ordered[i - 1].EndTime)
                            throw new Exception("Aynı gün içinde çakışan çalışma saati aralığı var.");
                    }
                }
            }
        }
    }
}
