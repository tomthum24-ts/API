using API.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class JobsConfiguration : IEntityTypeConfiguration<Jobs>
    {
        public void Configure(EntityTypeBuilder<Jobs> builder)
        {
            builder.ToTable(TableConstants.JOBS_TABLENAME);
            builder.Property(x => x.Code).HasField("_code").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Name).HasField("_name").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Category).HasField("_category").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimeStart).HasField("_timeStart").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimeZone).HasField("_timeZone").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.JobNumber).HasField("_jobNumber").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.JobNumberRemain).HasField("_jobNumberRemain").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsHome).HasField("_isHome").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsEating).HasField("_isEating").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Province).HasField("_province").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.District).HasField("_district").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Village).HasField("_village").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Address).HasField("_address").HasMaxLength(1000).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TypeJob).HasField("_typeJob").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.PriceFrom).HasField("_priceFrom").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.PriceTo).HasField("_priceTo").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TypePayment).HasField("_typePayment").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimePayment).HasField("_timePayment").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Detail).HasField("_detail").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Require).HasField("_require").HasMaxLength(1000).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Cancel).HasField("_cancel").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Tools).HasField("_tools").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
