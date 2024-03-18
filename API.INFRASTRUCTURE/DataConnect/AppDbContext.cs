using API.DOMAIN;
using API.DOMAIN.DomainObjects.BieuMau;
using API.DOMAIN.DomainObjects.WareHouseInDetail;
using API.DOMAIN.DomainObjects.WareHouseOut;
using API.DOMAIN.DomainObjects.WareHouseOutDetail;
using API.INFRASTRUCTURE.EFConfigs;
using API.INFRASTRUCTURE.EFConfigs.BieuMau;
using API.INFRASTRUCTURE.EFConfigs.Location;
using API.INFRASTRUCTURE.EFConfigs.Media;
using API.INFRASTRUCTURE.EFConfigs.Permission;
using Microsoft.EntityFrameworkCore;

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
            //User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.USER_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            //Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.PROJECT_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            //Province
            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.PROVINCE_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new ProvinceConfiguration());
            //Distrist
            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.DISTRICT_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            //Village
            modelBuilder.Entity<Village>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.VILLAGE_TABLENAME);
            });

            modelBuilder.ApplyConfiguration(new VillageConfiguration());
            //Refresh token

            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.REFRESHTOKEN_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());
            //Credential
            modelBuilder.Entity<PM_Credential>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.CREDENTIAL_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new CredentialConfiguration());
            //File Attach
            modelBuilder.Entity<AttachmentFile>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.AttachmentFile_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new AttachmentFileConfiguration());
            //Role permision
            modelBuilder.Entity<RolePermissions>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.ROLEPERMISSION_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            //Report
            modelBuilder.Entity<SysBieuMau>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.SYSBIEUMAU_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new SysBieuMauConfiguration());
            //Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.CUSTOMER_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //WareHouseIn
            modelBuilder.Entity<WareHouseIn>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.WAREHOUSEIN_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new WareHouseInConfiguration());
            //WareHouseIn_Detail
            modelBuilder.Entity<WareHouseInDetail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.WAREHOUSEINDETAIL_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new WareHouseInDetailConfiguration());
            //WareHouseOut
            modelBuilder.Entity<WareHouseOut>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.WAREHOUSEOUT_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new WareHouseOutConfiguration());
            //WareHouseOut_Detail
            modelBuilder.Entity<WareHouseOutDetail>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable(TableConstants.WAREHOUSEOUTDETAIL_TABLENAME);
            });
            modelBuilder.ApplyConfiguration(new WareHouseOutDetailConfiguration());
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
        public DbSet<Customer> Customer { get; set; }
        public DbSet<WareHouseIn> WareHouseIn { get; set; }
        public DbSet<WareHouseInDetail> WareHouseInDetail { get; set; }
        public DbSet<WareHouseOut> WareHouseOut { get; set; }
        public DbSet<WareHouseOutDetail> WareHouseOutDetail { get; set; }
    }
}