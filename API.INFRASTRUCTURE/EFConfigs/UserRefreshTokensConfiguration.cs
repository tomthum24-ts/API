using API.DOMAIN;
using API.HRM.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable(TableConstants.REFRESHTOKEN_TABLENAME);
            builder.Property(x => x.Token).HasField("_token").HasMaxLength(2000).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IdRefreshToken).HasField("_idRefreshToken").HasMaxLength(2000).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Expires).HasField("_expires").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IpAddress).HasField("_ipAddress").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
