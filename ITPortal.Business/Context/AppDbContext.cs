using ITPortal.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace ITPortal.Business.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> user_roles => Set<UserRole>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<LookupType> LookupTypes => Set<LookupType>();
        public DbSet<Lookup> Lookups => Set<Lookup>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketCategory> TicketCategories => Set<TicketCategory>();
        public DbSet<TicketComment> TicketComments => Set<TicketComment>();
        public DbSet<TicketEvent> TicketEvents  => Set<TicketEvent>();
        public DbSet<MajorIncident> MajorIncidents  => Set<MajorIncident>();
        public DbSet<MajorIncidentUpdate> MajorIncidentUpdates  => Set<MajorIncidentUpdate>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
