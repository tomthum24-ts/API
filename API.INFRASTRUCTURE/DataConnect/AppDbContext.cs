using API.HRM.DOMAIN;
using API.HRM.DOMAIN.DomainObjects.User;
using API.INFRASTRUCTURE.EFConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                entity.HasKey(e => e.id);
                entity.ToTable("user");
            });
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
        public DbSet<User> User { get; set; }

        //public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }
        //public virtual DbSet<UserRefreshTokens> UserRefreshToken { get; set; }

    }
}
