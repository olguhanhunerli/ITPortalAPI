using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.DepartmentDTOs;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentMiniDTO> CreateDepartmentAsync(CreateDepartmentDTO createDepartmentDTO)
        {
            var mappedDepartment = _mapper.Map<Department>(createDepartmentDTO);
            mappedDepartment.CreatedAt = DateTime.UtcNow;
            await _departmentRepository.AddAsync(mappedDepartment);
            await _departmentRepository.SaveChangesAsync();
            return _mapper.Map<DepartmentMiniDTO>(mappedDepartment);
        }

        public async Task<bool> DeleteDepartmentAsync(ulong id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
                throw new KeyNotFoundException($"Department not found. Id: {id}");
            _departmentRepository.Remove(department);
            await _departmentRepository.SaveChangesAsync();
            return true;

        }

        public async Task<DepartmentDTO> GetDepartmentByIdAsync(ulong id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
                throw new KeyNotFoundException($"Department not found. Id: {id}");
            return _mapper.Map<DepartmentDTO>(department);
        }

        public async Task<List<DepartmentLookUpDTO>> GetDepartmentLookUpAsync(string? search, int take)
        {
            return await _departmentRepository.GetDepartmentLookUpAsync(search, take);
        }

        public async Task<PagedResultDTO<DepartmentMiniDTO>> GetDepartmentsWithPaginationAsync(int pageNumber, int pageSize)
        {
            var pagedDepartments = await _departmentRepository.GetDepartmentsWithPaginationAsync(pageNumber, pageSize);
            var mappedDepartments = _mapper.Map<List<DepartmentMiniDTO>>(pagedDepartments.Items);

            return new PagedResultDTO<DepartmentMiniDTO> 
            {
                TotalCount = pagedDepartments.TotalCount,
                Page = pagedDepartments.Page,
                PageSize = pagedDepartments.PageSize,
                Items = mappedDepartments
            };
        }

        public async Task<DepartmentMiniDTO> UpdateDepartmentAsync(ulong id, UpdateDepartmentDTO updateDepartmentDTO)
        {
            var departmentEntity = await _departmentRepository.GetDepartmentByIdAsync(id);
            var entity = new Department
            {
                Id = departmentEntity.Id,
                Name = updateDepartmentDTO.Name,
                CreatedAt = departmentEntity.CreatedAt,
                Users = departmentEntity.Users,
                Teams = departmentEntity.Teams
            };
            _departmentRepository.Update(entity);
            await _departmentRepository.SaveChangesAsync();
            return _mapper.Map<DepartmentMiniDTO>(entity);

        }
    }
}
