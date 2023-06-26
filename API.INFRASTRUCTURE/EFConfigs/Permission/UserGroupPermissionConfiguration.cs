using API.DOMAIN;
using API.DOMAIN.DomainObjects.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs.Permission
{
    public class UserGroupPermissionConfiguration : IEntityTypeConfiguration<UserGroupPermission>
    {
        public void Configure(EntityTypeBuilder<UserGroupPermission> builder)
        {
            builder.ToTable(TableConstants.USERPERMISSION_TABLENAME);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(500).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
