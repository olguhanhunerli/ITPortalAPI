using AutoMapper;
using ITPortal.Entities.DTOs.DepartmentDTOs;
using ITPortal.Entities.DTOs.LocationDTOs;
using ITPortal.Entities.DTOs.LookupDTOs;
using ITPortal.Entities.DTOs.MajorIncidentDTOs;
using ITPortal.Entities.DTOs.RoleDTOs;
using ITPortal.Entities.DTOs.ServiceDTOs;
using ITPortal.Entities.DTOs.TeamDTOs;
using ITPortal.Entities.DTOs.TicketCategoryDTOs;
using ITPortal.Entities.DTOs.TicketCommentDTOs;
using ITPortal.Entities.DTOs.TicketDTOs;
using ITPortal.Entities.DTOs.TicketEventDTOs;
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

            CreateMap<Ticket, TicketDetailDTO>()
               .ForMember(d => d.MajorIncidentId, opt => opt.MapFrom(s => s.MajorIncident != null ? (ulong?)s.MajorIncident.Id : null))
               .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.NameTr : null))
               .ForMember(d => d.SubcategoryName, opt => opt.MapFrom(s => s.Subcategory != null ? s.Subcategory.NameTr : null))
               .ForMember(d => d.TypeName, opt => opt.MapFrom(s => s.Type.NameTr))
               .ForMember(d => d.StatusName, opt => opt.MapFrom(s => s.Status.NameTr))
               .ForMember(d => d.ServiceName, opt => opt.MapFrom(s => s.Service.NameTr))
               .ForMember(d => d.PriorityName, opt => opt.MapFrom(s => s.Priority != null ? s.Priority.NameTr : null))
               .ForMember(d => d.ImpactName, opt => opt.MapFrom(s => s.Impact != null ? s.Impact.NameTr : null))
               .ForMember(d => d.ConfigurationItemName, opt => opt.MapFrom(s => s.ConfigurationItem.Name != null ? s.ConfigurationItem.NameTr : null))
               .ForMember(d => d.UrgencyName, opt => opt.MapFrom(s => s.Urgency != null ? s.Urgency.NameTr : null))
               .ForMember(d => d.ApprovalStateName, opt => opt.MapFrom(s => s.ApprovalState != null ? s.ApprovalState.NameTr : null))
               .ForMember(d => d.RequesterName, opt => opt.MapFrom(s => s.Requester.FullName))
               .ForMember(d => d.RequestedForName, opt => opt.MapFrom(s => s.RequestedFor != null ? s.RequestedFor.FullName : null))
               .ForMember(d => d.AssigneeName, opt => opt.MapFrom(s => s.Assignee != null ? s.Assignee.FullName : null))
               .ForMember(d => d.AssignedTeamName, opt => opt.MapFrom(s => s.AssignedTeam != null ? s.AssignedTeam.Name : null))
               .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department != null ? s.Department.Name : null))
               .ForMember(d => d.LocationName, opt => opt.MapFrom(s => s.Location != null ? s.Location.Name : null))
               .ForMember(d => d.Comments, opt => opt.MapFrom(s => s.Comments))
               .ForMember(d => d.Events, opt => opt.MapFrom(s => s.Events));
            CreateMap<CreateTicketDTO, Ticket>();
            CreateMap<Ticket, TicketMiniDTO>()
                .ForMember(d => d.PriorityName, opt => opt.MapFrom(s => s.Priority != null ? s.Priority.NameTr : null))
                .ForMember(d => d.RequesterName, opt => opt.MapFrom(s => s.Requester != null ? s.Requester.FullName : null))
                .ForMember(d => d.AssigneeName, opt => opt.MapFrom(s => s.Assignee != null ? s.Assignee.FullName : null))
                .ForMember(d => d.StatusName, opt => opt.MapFrom(s => s.Status != null ? s.Status.NameTr : null))
                .ReverseMap();
            CreateMap<UpdateTicketAssignmentDTO, Ticket>()
                .ForMember(d => d.AssigneeId, opt => opt.MapFrom(s => s.AssigneeId));
            CreateMap<TicketComment, TicketCommentDTO>()
                .ForMember(d => d.TicketName, o => o.MapFrom(s => s.Ticket.Title)) 
                .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.FullName))
                .ForMember(d => d.VisibilityName, o => o.MapFrom(s => s.Visibility.NameTr));
            CreateMap<CreateCommentDTO, TicketComment>();

            CreateMap<TicketEvent, TicketEventDTO>()
                .ForMember(d => d.ActorName, opt => opt.MapFrom(s => s.Actor != null ? s.Actor.FullName : null));

            CreateMap<MajorIncident, MajorIncidentSummaryDTO>()
                .ForMember(d => d.StatusName, opt => opt.MapFrom(s => s.Status.NameTr))
                .ForMember(d => d.LeadName, opt => opt.MapFrom(s => s.Lead.FullName));

            CreateMap<Service, ServiceDetailDTO>()
                .ForMember(d => d.OwnerTeamName, opt => opt.MapFrom(s => s.OwnerTeam != null ? s.OwnerTeam.Name : null)).ReverseMap();
            CreateMap<ServiceDetailDTO, Service>().ReverseMap();
            CreateMap<ServiceMiniDTO, Service>().ReverseMap();
            CreateMap<CreateServiceDTO, Service>().ReverseMap();
            CreateMap<UpdateServiceDTO, Service>().ReverseMap();
            CreateMap<ServiceLookupDTO, Service>().ReverseMap();
        }
    }
}
