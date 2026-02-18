using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.DepartmentDTOs;
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
    }
}
