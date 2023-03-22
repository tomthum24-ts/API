using API.DOMAIN.DomainObjects.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class UserRefreshTokensConfiguration : IEntityTypeConfiguration<UserRefreshTokens>
    {
        public void Configure(EntityTypeBuilder<UserRefreshTokens> builder)
        {
            builder.ToTable("UserRefreshTokens");
            builder.Property(x => x.Id).HasField("_id").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.UserName).HasField("_userName").HasMaxLength(255).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.RefreshToken).HasField("_refreshToken").HasMaxLength(255).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsActive).HasField("_isActive").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CreatedDate).HasField("_createdDate").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CreatedBy).HasField("_createdBy").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ModifiedDate).HasField("_modifiedDate").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ModifiedBy).HasField("_modifiedBy").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
