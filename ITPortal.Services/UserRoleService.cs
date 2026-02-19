using AutoMapper;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.UserRoleDTOs;
using ITPortal.Entities.Model;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository, IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> AssignRoleAsync(ulong userId, ulong roleId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }


            if(await _userRoleRepository.ExistsAsync(userId, roleId))
            {
                return false;
            }

            await _userRoleRepository.AddAsync(new UserRole
            {
                UserId = userId,
                RoleId = roleId,
                AssignedAt = DateTime.UtcNow,
            });

            await _userRoleRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserRoleDTO>> GetUserRolesAsync(ulong userId)
        {
            return await _userRoleRepository.GetUserRolesAsync(userId); 
        }

        public async Task<bool> RemoveUserRoleAsync(ulong userId,ulong roleId)
        {
            var result = await _userRoleRepository.RemoveAsync(userId, roleId);
            if(!result)
            {
                return false;
            }
            await _userRoleRepository.SaveChangesAsync();
            return true;
        }
    }
}
