using API.INFRASTRUCTURE.EFConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using API.DOMAIN;

namespace API.INFRASTRUCTURE.DataConnect
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("Default"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("DA_User");
            });
           
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("DA_Project");
            });
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        }
        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }

        //public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }

    }
}
