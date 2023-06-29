using API.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable(TableConstants.MENU_TABLENAME);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ParentID).HasField("_parentID").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Link).HasField("_link").HasMaxLength(1000).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Image).HasField("_image").HasMaxLength(500).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Status).HasField("_status").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
