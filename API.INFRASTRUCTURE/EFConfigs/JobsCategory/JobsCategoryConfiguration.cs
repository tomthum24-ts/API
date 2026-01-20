using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using API.DOMAIN;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class JobsCategoryConfiguration : IEntityTypeConfiguration<JobsCategory>
    {
        public void Configure(EntityTypeBuilder<JobsCategory> builder)
        {
            builder.ToTable(TableConstants.JOBSCATEGORY_TABLENAME);
            builder.Property(x => x.Code).HasField("_code").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasMaxLength(2000).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
