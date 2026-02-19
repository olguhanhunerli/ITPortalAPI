using AutoMapper;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.DTOs.RoleDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Entities.Model;

namespace ITPortal.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserMiniDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.Name : null))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : null))
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.UserRoles.Select(ur => ur.Role.Name))); 
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name != null ? src.Department.Name : null))
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => s.UserRoles.Select(ur => ur.Role.Name)));


            CreateMap<Department, DepartmentMiniDTO>()
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count))
                .ForMember(dest => dest.TeamCount, opt => opt.MapFrom(src => src.Teams.Count));
            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<UpdateDepartmentDTO, Department>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentDTO>();

            CreateMap<Team, TeamMiniDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count));
            CreateMap<CreateTeamDTO, Team>();
            CreateMap<UpdateTeamDTO, Team>();
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name: null));
            CreateMap<TeamDTO, Team>();


            CreateMap<Location, LocationMiniDTO>()
                 .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count)); ;
            CreateMap<CreateLocationDTO, Location>();
            CreateMap<UpdateLocationDTO, Location>();
            CreateMap<Location, LocationDTO>();
            CreateMap<LocationDTO, Location>();

            CreateMap<Role, RoleMiniDTO>();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<UpdateRoleDTO, Role>();
            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();    
        }
    }
}
