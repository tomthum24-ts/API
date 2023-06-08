using API.INFRASTRUCTURE.EFConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using API.INFRASTRUCTURE.EFConfigs.Location;
using API.DOMAIN;

namespace API.INFRASTRUCTURE.DataConnect
{
    public class IDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public IDbContext(IConfiguration configuration)
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
                entity.ToTable(TableConstants.USER_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.PRỌJECT_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.Province_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.District_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());

            modelBuilder.Entity<Village>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.Village_TABLENAME);
            });
            
            modelBuilder.ApplyConfiguration(new VillageConfiguration());


            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.REFRESHTOKEN_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());
        }
        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Village> Village { get; set; }
        public DbSet<RefreshToken> UserRefreshToken { get; set; }

    }
}
