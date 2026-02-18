using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.Common;
using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResultDTO<UserMiniDTO>> GetUsersWithPaginationAsync(int pageNumber, int pageSize)
        {
            var pagedUsers = await _userRepository.GetUsersWithPaginationAsync(pageNumber, pageSize);
            var mappedUsers = _mapper.Map<List<UserMiniDTO>>(pagedUsers.Items);

            return new PagedResultDTO<UserMiniDTO>
            {
                TotalCount = pagedUsers.TotalCount,
                Page = pagedUsers.Page,
                PageSize = pagedUsers.PageSize,
                Items = mappedUsers
            };
        }
    }
}
