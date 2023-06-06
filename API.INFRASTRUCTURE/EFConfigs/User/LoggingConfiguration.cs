
using API.DOMAIN.DomainObjects.User;
using API.HRM.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs.User
{
    public class LoggingConfiguration : IEntityTypeConfiguration<Logging>
    {
        public void Configure(EntityTypeBuilder<Logging> builder)
        {
            builder.ToTable(TableConstants.LOGGING_TABLENAME);
            builder.Property(x => x.Tooken).HasField("_tooken").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Expired).HasField("_expired").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsActive).HasField("_isActive").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Devices).HasField("_devices").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IpAddress).HasField("_ipAddress").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
