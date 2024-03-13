using API.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class WareHouseInConfiguration : IEntityTypeConfiguration<WareHouseIn>
    {
        public void Configure(EntityTypeBuilder<WareHouseIn> builder)
        {
            builder.ToTable(TableConstants.WAREHOUSEIN_TABLENAME);

            builder.Property(x => x.Code).HasField("_code").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.DateCode).HasField("_dateCode").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CustomerID).HasField("_customerID").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Representative).HasField("_representative").HasMaxLength(100).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IntendTime).HasField("_intendTime").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.WareHouse).HasField("_wareHouse").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.OrtherNote).HasField("_ortherNote").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.FileAttach).HasField("_fileAttach").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}