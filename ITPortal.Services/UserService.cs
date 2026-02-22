using AutoMapper;
using ITPortal.Business.Repository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Entities.Model;
using ITPortal.Entities.Model.Common;
using ITPortal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(IUserRepository userRepository, IMapper mapper, ILocationRepository locationRepository, ITeamRepository teamRepository, IDepartmentRepository departmentRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _teamRepository = teamRepository;
            _departmentRepository = departmentRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrWhiteSpace(createUserDTO.Email))
                throw new ArgumentException("Email boş olamaz.");

            if (string.IsNullOrWhiteSpace(createUserDTO.Password))
                throw new ArgumentException("Password boş olamaz.");

            if (createUserDTO.DepartmentId == null)
                throw new ArgumentException("Department boş geçilemez.");

            if (createUserDTO.TeamId == null)
                throw new ArgumentException("Team boş geçilemez.");

            if (createUserDTO.LocationId == null)
                throw new ArgumentException("Location boş geçilemez.");

            var existing = await _userRepository.GetUserByEmailWithDetailsAsync(createUserDTO.Email);
            if (existing != null)
                throw new InvalidOperationException("Email sistemde kayıtlıdır.");

            var deptOk = await _departmentRepository.ExistsAsync(createUserDTO.DepartmentId.Value);
            if (!deptOk)
                throw new InvalidOperationException("Department bulunamadı.");

            var teamOk = await _teamRepository.ExistsAsync(createUserDTO.TeamId.Value);
            if (!teamOk)
                throw new InvalidOperationException("Team bulunamadı.");

            var locOk = await _locationRepository.ExistsAsync(createUserDTO.LocationId.Value);
            if (!locOk)
                throw new InvalidOperationException("Location bulunamadı.");

            var teamBelongs = await _teamRepository.TeamBelongsToDepartmentAsync(
                createUserDTO.TeamId.Value,
                createUserDTO.DepartmentId.Value);

            if (!teamBelongs)
                throw new InvalidOperationException("Team seçilen department'a ait değil.");

            var userEntity = _mapper.Map<User>(createUserDTO);
            userEntity.CreatedAt = DateTime.UtcNow;
            userEntity.UpdatedAt = DateTime.UtcNow;
            userEntity.IsActive = true;

            userEntity.AuthType = AuthType.Local;
            userEntity.ExternalProvider = null;
            userEntity.ExternalId = null;
            userEntity.LastLoginAt = null;

            

            var hash = Hash(createUserDTO.Password, out var salt);
            userEntity.PasswordHash = $"HMACSHA256.{salt}.{hash}";

            await _userRepository.AddAsync(userEntity);
            await _userRepository.SaveChangesAsync();
            
            var employeeRole = await _roleRepository.GetByNameAsync("Employee");
            if (employeeRole == null)
                throw new InvalidOperationException("Employee rolü bulunamadı.");

            var already = await _userRoleRepository.ExistsAsync(userEntity.Id, employeeRole.Id);
            if (!already)
            {
                await _userRoleRepository.AddAsync(new UserRole
                {
                    UserId = userEntity.Id,
                    RoleId = employeeRole.Id,
                    AssignedAt = DateTime.UtcNow
                });

                await _userRoleRepository.SaveChangesAsync();
            }
            var createdUser = await _userRepository.GetUserByEmailWithDetailsAsync(userEntity.Email);
            return _mapper.Map<UserDTO>(createdUser);
        }

        public async Task<UserDTO> GetUserByEmailWithDetailsAsync(string email)
        {
            var userEntity = await _userRepository.GetUserByEmailWithDetailsAsync(email);
            if (userEntity == null)
            {
                return null;
            }
            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<List<UserLookUpDTO>> GetUserLookUpAsync(string? search, int take)
        {
            return await _userRepository.GetUserLookUpAsync(search, take);
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
        private string Hash(string password, out string salt)
        {
            using var hmac = new HMACSHA256();
            salt = Convert.ToBase64String(hmac.Key);
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

    }
}
