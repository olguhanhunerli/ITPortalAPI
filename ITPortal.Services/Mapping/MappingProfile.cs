using AutoMapper;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.DTOs.LookupDTOs;
using ITPortal.Entities.DTOs.RoleDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.DTOs.TicketCategoryDTOs;
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
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => (s.UserRoles ?? new List<UserRole>()).Where(ur => ur.Role != null).Select(ur => ur.Role!.Name).ToList()));
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(d => d.Roles, opt => opt.MapFrom(s => (s.UserRoles ?? new List<UserRole>()).Where(ur => ur.Role != null).Select(ur => ur.Role!.Name).ToList()));

            CreateMap<Department, DepartmentMiniDTO>()
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count))
                .ForMember(dest => dest.TeamCount, opt => opt.MapFrom(src => src.Teams.Count));
            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<UpdateDepartmentDTO, Department>();
            CreateMap<DepartmentDTO, Department>().ReverseMap();

            CreateMap<Team, TeamMiniDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count));
            CreateMap<CreateTeamDTO, Team>();
            CreateMap<UpdateTeamDTO, Team>();
            CreateMap<Team, TeamDTO>()
                 .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department != null ? s.Department.Name : null))
                 .ReverseMap();


            CreateMap<Location, LocationMiniDTO>()
                 .ForMember(dest => dest.UserCount, opt => opt.MapFrom(src => src.Users.Count));
            CreateMap<CreateLocationDTO, Location>();
            CreateMap<UpdateLocationDTO, Location>();
            CreateMap<Location, LocationDTO>().ReverseMap();

            CreateMap<Role, RoleMiniDTO>();
            CreateMap<CreateRoleDTO, Role>();
            CreateMap<UpdateRoleDTO, Role>();
            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<LookupType, LookupTypeLookupDTO>();
            CreateMap<Lookup, LookupLookupDTO>();
            CreateMap<Lookup, LookupDTO>()
                .ForMember(d => d.LookupTypeCode, opt => opt.MapFrom(s => s.LookupType != null ? s.LookupType.Code : null));

            CreateMap<CreateTicketCategoryDTO, TicketCategory>();
            CreateMap<UpdateTicketCategoryDTO, TicketCategory>();

            CreateMap<TicketCategory, TicketCategoryDTO>()
                .ForMember(d => d.ParentName, o => o.MapFrom(s => s.Parent != null ? s.Parent.Code : null))
                .ForMember(d => d.DefaultTeamName, o => o.MapFrom(s => s.DefaultTeam != null ? s.DefaultTeam.Name : null));
        }
    }
}
