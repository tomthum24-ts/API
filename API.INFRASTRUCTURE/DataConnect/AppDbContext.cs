using API.INFRASTRUCTURE.EFConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using API.INFRASTRUCTURE.EFConfigs.Location;
using API.DOMAIN;
using Microsoft.AspNetCore.Hosting;
using API.INFRASTRUCTURE.EFConfigs.Permission;
using API.INFRASTRUCTURE.EFConfigs.Media;
using API.DOMAIN.DomainObjects.BieuMau;
using API.INFRASTRUCTURE.EFConfigs.BieuMau;

namespace API.INFRASTRUCTURE.DataConnect
{
    public class IDbContext : DbContext
    {
        private readonly GetConnectString _getConnectString;

        public IDbContext(GetConnectString getConnectString)
        {
            _getConnectString = getConnectString;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(_getConnectString.GetConnect());
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
                entity.ToTable(TableConstants.PROVINCE_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.DISTRICT_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());

            modelBuilder.Entity<Village>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.VILLAGE_TABLENAME);
            });
            
            modelBuilder.ApplyConfiguration(new VillageConfiguration());


            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.REFRESHTOKEN_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());

            modelBuilder.Entity<PM_Credential>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.CREDENTIAL_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new CredentialConfiguration());

            modelBuilder.Entity<AttachmentFile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.AttachmentFile_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new AttachmentFileConfiguration());

            modelBuilder.Entity<RolePermissions>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.ROLEPERMISSION_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());

            modelBuilder.Entity<SysBieuMau>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.SYSBIEUMAU_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new SysBieuMauConfiguration());
        }
        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Village> Village { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }
        public DbSet<PM_Credential> Credential { get; set; }
        public DbSet<AttachmentFile> AttachmentFile { get; set; }
        public DbSet<SysBieuMau> SysBieuMau { get; set; }

    }
}
