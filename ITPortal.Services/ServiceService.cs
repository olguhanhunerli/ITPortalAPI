using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.ServiceDTOs;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<ServiceDetailDTO> CreateServicesAsync(CreateServiceDTO createServiceDTO)
        {
            var now = DateTime.Now;
            var entity = _mapper.Map<Entities.Model.Service>(createServiceDTO);
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
            await _serviceRepository.AddAsync(entity);
            await _serviceRepository.SaveChangesAsync();
            var created = await _serviceRepository.GetByServiceIdAsync(entity.Id);
            if (created == null) throw new Exception("Service oluşturuldu ama okunamadı.");

            return _mapper.Map<ServiceDetailDTO>(created);

        }

        public async Task<bool> DeleteServiceAsync(ulong serviceId)
        {
            var result = await _serviceRepository.GetByServiceIdAsync(serviceId);
            if (result == null)
                return false;
            result.IsActive = false;
            result.DeletedAt = DateTime.UtcNow;
            _serviceRepository.Update(result);
            await _serviceRepository.SaveChangesAsync();
            return true;
        }

        public async Task<ServiceDetailDTO> GetByServiceIdAsync(ulong serviceId)
        {
            var entity = await _serviceRepository.GetByServiceIdAsync(serviceId);
            if (entity == null)
                throw new Exception("Service Bulunamadı.");
            return _mapper.Map<ServiceDetailDTO>(entity);
        }

        public async Task<List<ServiceLookupDTO>> GetLookupServiceAsync()
        {
            var list = await _serviceRepository.GetServiceLookupAsync();
            return _mapper.Map<List<ServiceLookupDTO>>(list);
        }

        public async Task<PagedResultDTO<ServiceMiniDTO>> GetServicesWithPaginationAsync(int pageNumber, int pageSize)
        {
            var paged = await _serviceRepository.GetAllServiceAsync(pageNumber, pageSize);

            return new PagedResultDTO<ServiceMiniDTO> 
                {
                TotalCount = paged.TotalCount,
                Page = pageNumber,
                PageSize = pageSize,
                Items = _mapper.Map<List<ServiceMiniDTO>>(paged.Items)
            };

        }

        public async Task<bool> UpdateServiceActiveAsync(ulong serviceId)
        {
            var entity = await _serviceRepository.GetByServiceIdAsync(serviceId);
            entity.IsActive = !entity.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            _serviceRepository.Update(entity);
            await _serviceRepository.SaveChangesAsync();
            return true;

        }

        public async Task<ServiceDetailDTO> UpdateServiceAsync(ulong serviceId, UpdateServiceDTO updateServiceDTO)
        {
            var entity = await _serviceRepository.GetByServiceIdAsync(serviceId);
            if (entity == null)
                throw new Exception("Service Bulunamadı.");
            _mapper.Map(updateServiceDTO, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            _serviceRepository.Update(entity);
            await _serviceRepository.SaveChangesAsync();

            var updated = await _serviceRepository.GetByServiceIdAsync(serviceId);
            return _mapper.Map<ServiceDetailDTO>(updated);
        }
    }
}
