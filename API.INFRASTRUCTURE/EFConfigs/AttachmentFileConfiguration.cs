using API.DOMAIN.DomainObjects.Media;
using API.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs
{
    internal class AttachmentFileConfiguration : IEntityTypeConfiguration<AttachmentFile>
    {
        public void Configure(EntityTypeBuilder<AttachmentFile> builder)
        {
            builder.ToTable(TableConstants.AttachmentFile_TABLENAME);
            builder.Property(x => x.Id).HasField("_id").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(255).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Type).HasField("_type").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Size).HasField("_size").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Path).HasField("_path").HasMaxLength(255).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ForWeb).HasField("_forWeb").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CheckSum).HasField("_checkSum").HasMaxLength(255).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
