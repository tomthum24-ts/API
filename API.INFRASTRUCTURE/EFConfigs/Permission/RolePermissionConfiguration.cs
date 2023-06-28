using API.DOMAIN;
using API.DOMAIN.DomainObjects.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs.Permission
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable(TableConstants.ROLEPERMISSION_TABLENAME);
            builder.Property(x => x.NameController).HasField("_nameController").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ActionName).HasField("_actionName").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
