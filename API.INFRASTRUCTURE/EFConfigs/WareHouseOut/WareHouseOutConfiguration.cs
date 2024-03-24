using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseOut;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class WareHouseOutConfiguration : IEntityTypeConfiguration<WareHouseOut>
    {
        public void Configure(EntityTypeBuilder<WareHouseOut> builder)
        {
            builder.ToTable(TableConstants.WAREHOUSEOUT_TABLENAME);
            builder.Property(x => x.Code).HasField("_code").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.DateCode).HasField("_dateCode").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CustomerID).HasField("_customerID").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Representative).HasField("_representative").HasMaxLength(100).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IntendTime).HasField("_intendTime").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.WareHouse).HasField("_wareHouse").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CustomerName).HasField("_customerName").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.FilePath).HasField("_filePath").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.FileName).HasField("_fileName").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Seal).HasField("_seal").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Temp).HasField("_temp").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CarNumber).HasField("_carNumber").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Container).HasField("_container").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Door).HasField("_door").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Deliver).HasField("_deliver").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Veterinary).HasField("_veterinary").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Cont).HasField("_cont").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.OrtherNote).HasField("_ortherNote").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.FileAttach).HasField("_fileAttach").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.NumberCode).HasField("_numberCode").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.InvoiceNumber).HasField("_invoiceNumber").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimeStart).HasField("_timeStart").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TimeEnd).HasField("_timeEnd").UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
