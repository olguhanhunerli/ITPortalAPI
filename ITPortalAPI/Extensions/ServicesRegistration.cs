using AutoMapper;
using ITPortal.Business.Context;
using ITPortal.Business.Repository;
using ITPortal.Business.Repository.GenericRepository;
using ITPortal.Business.Repository.Interfaces;
using ITPortal.Entities.Model;
using ITPortal.Services;
using ITPortal.Services.Interfaces;
using ITPortal.Services.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace ITPortalAPI.Extensions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 32))
                )
            );
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(ITPortal.Services.Mapping.MappingProfile).Assembly);
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped<IUserServices, UserService>();

            services.AddScoped(typeof(ITeamRepository), typeof(TeamRepository));
            services.AddScoped<ITeamService, TeamService>();

            services.AddScoped(typeof(ILocationRepository), typeof(LocationRepository));
            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped(typeof(IUserRoleRepository), typeof(UserRoleRepository));
            services.AddScoped<IUserRoleService, UserRoleService>();

            services.AddScoped(typeof(IDepartmentRepository), typeof(DepartmentRepository));
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddScoped(typeof(ILookupRepository), typeof(LookupRepository));
            services.AddScoped<ILookupService, LookupService>();

            services.AddScoped(typeof(ITicketRepository), typeof(TicketRepository));
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped(typeof(ITicketCategoryRepository), typeof(TicketCategoryRepository));
            services.AddScoped<ITicketCategoryService, TicketCategoryService>();

            services.AddScoped(typeof(ITicketCommentRepository), typeof(TicketCommentRepository));
            services.AddScoped<ITicketCommentService, TicketCommentService>();

            services.AddScoped(typeof(IServiceRepository), typeof(ServiceRepository));
            services.AddScoped<IServiceService, ServiceService>();

            services.AddScoped(typeof(IConfigurationItemsRepository), typeof(ConfigurationItemsRepository));
            services.AddScoped<IConfigurationItemService, ConfigurationItemService>();

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped(typeof(ITicketEventRepository), typeof(TicketEventRepository));
            services.AddScoped<ITicketEventService, TicketEventService>();

            services.AddScoped(typeof(ITicketAssignmentHistoryRepository), typeof(TicketAssignmentHistoryRepository));
            services.AddScoped<ITicketAssignmentHistoryService, TicketAssignmentHistoryService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

           

            

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwt = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var key = Encoding.UTF8.GetBytes(jwt.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.MapInboundClaims = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ClockSkew = TimeSpan.Zero,

                    NameClaimType = JwtRegisteredClaimNames.Sub,
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                };
            });
            return services;

        }

    }
}
