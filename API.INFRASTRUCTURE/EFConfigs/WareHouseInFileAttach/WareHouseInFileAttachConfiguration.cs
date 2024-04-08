using API.DOMAIN;
using API.DOMAIN.DomainObjects;
using API.DOMAIN.DomainObjects.WareHouseInFileAttach;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class WareHouseInFileAttachConfiguration : IEntityTypeConfiguration<WareHouseInFileAttachs>
    {
        public void Configure(EntityTypeBuilder<WareHouseInFileAttachs> builder)
        {
            builder.ToTable(TableConstants.WAREHOUSEINFILEATTACH_TABLENAME);
            builder.Property(x => x.IdWareHouseIn).HasField("_idWareHouseIn").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Path).HasField("_path").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
