using API.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable(TableConstants.REFRESHTOKEN_TABLENAME);
            builder.Property(x => x.IdRefreshToken).HasField("_idRefreshToken").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Expires).HasField("_expires").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IpAddress).HasField("_ipAddress").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.UserLogin).HasField("_userLogin").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Revoked).HasField("_revoked").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.RevokedByIp).HasField("_revokedByIp").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ReplacedByToken).HasField("_replacedByToken").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ReasonRevoked).HasField("_reasonRevoked").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsRevoked).HasField("_isRevoked").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsActive).HasField("_isActive").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimeLogout).HasField("_timeLogout").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.UserAgent).HasField("_userAgent").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Type).HasField("_type").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.OSName).HasField("_oSName").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.OSVersion).HasField("_oSVersion").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.DeviceHash).HasField("_deviceHash").HasMaxLength(500).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.BrownName).HasField("_brownName").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.BrownVersion).HasField("_brownVersion").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimeZone).HasField("_timeZone").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsLogout).HasField("_isLogout").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
