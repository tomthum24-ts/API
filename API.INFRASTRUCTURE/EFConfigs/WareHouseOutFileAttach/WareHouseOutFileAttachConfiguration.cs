using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseOutFileAttach;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace API.INFRASTRUCTURE.EFConfigss
{
    internal class WareHouseOutFileAttachConfiguration : IEntityTypeConfiguration<WareHouseOutFileAttachs>
    {
        public void Configure(EntityTypeBuilder<WareHouseOutFileAttachs> builder)
        {
            builder.ToTable(TableConstants.WAREHOUSEOUTFILEATTACH_TABLENAME);
            builder.Property(x => x.IdWareHouseOut).HasField("_idWareHouseOut").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Path).HasField("_path").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}