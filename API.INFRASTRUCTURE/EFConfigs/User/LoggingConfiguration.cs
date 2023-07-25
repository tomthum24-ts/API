using API.DOMAIN;
using API.DOMAIN.DomainObjects.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs.User
{
    public class LoggingConfiguration : IEntityTypeConfiguration<Logging>
    {
        public void Configure(EntityTypeBuilder<Logging> builder)
        {
            builder.ToTable(TableConstants.LOGGING_TABLENAME);
            builder.Property(x => x.Token).HasField("_token").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Expired).HasField("_expired").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsActive).HasField("_isActive").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Devices).HasField("_devices").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IpAddress).HasField("_ipAddress").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.BrownserName).HasField("_brownserName").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.BrownserVersion).HasField("_brownserVersion").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.DeviceHash).HasField("_deviceHash").HasMaxLength(500).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.EngineName).HasField("_engineName").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.EngineVersion).HasField("_engineVersion").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.OsName).HasField("_osName").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.OsVersion).HasField("_osVersion").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimeZone).HasField("_timeZone").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Type).HasField("_type").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.UserAgent).HasField("_userAgent").HasMaxLength(500).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
