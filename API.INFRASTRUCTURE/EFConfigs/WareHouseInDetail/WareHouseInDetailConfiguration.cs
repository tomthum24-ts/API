using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseInDetail;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class WareHouseInDetailConfiguration : IEntityTypeConfiguration<WareHouseInDetail>
    {
        public void Configure(EntityTypeBuilder<WareHouseInDetail> builder)
        {
            builder.ToTable(TableConstants.WAREHOUSEINDETAIL_TABLENAME);
            builder.Property(x => x.IdWareHouseIn).HasField("_idWareHouseIn").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.RangeOfVehicle).HasField("_rangeOfVehicle").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.QuantityVehicle).HasField("_quantityVehicle").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.ProductId).HasField("_productId").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.QuantityProduct).HasField("_quantityProduct").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Unit).HasField("_unit").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Size).HasField("_size").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Weight).HasField("_weight").UsePropertyAccessMode(PropertyAccessMode.Field);
            
        }
    }
}
