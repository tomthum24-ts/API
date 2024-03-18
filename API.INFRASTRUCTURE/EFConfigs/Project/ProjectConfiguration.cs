using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using API.DOMAIN;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable(TableConstants.PROJECT_TABLENAME);
            builder.Property(x => x.ProjectCode).HasField("_projectCode").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ProjectName).HasField("_projectName").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasMaxLength(2000).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Status).HasField("_status").UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
