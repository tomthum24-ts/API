using API.DOMAIN;
using API.DOMAIN.DomainObjects.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace API.INFRASTRUCTURE.EFConfigs.Permission
{
    public class CredentialConfiguration : IEntityTypeConfiguration<Credential>
    {
        public void Configure(EntityTypeBuilder<Credential> builder)
        {
            builder.ToTable(TableConstants.CREDENTIAL_TABLENAME);
            builder.Property(x => x.UserGroupId).HasField("_userGroupId").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.RoleId).HasField("_roleId").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
