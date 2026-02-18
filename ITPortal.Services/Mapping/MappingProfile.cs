using AutoMapper;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.DTOs.UserDTOs;
using ITPortal.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserMiniDTO>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team != null ? src.Team.Name : null))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location != null ? src.Location.Name : null));
           

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
        }
    }
}
