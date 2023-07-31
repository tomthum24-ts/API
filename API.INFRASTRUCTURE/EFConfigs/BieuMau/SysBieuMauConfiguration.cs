using API.DOMAIN;
using API.DOMAIN.DomainObjects.BieuMau;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs.BieuMau
{
    public class SysBieuMauConfiguration : IEntityTypeConfiguration<SysBieuMau>
    {
        public void Configure(EntityTypeBuilder<SysBieuMau> builder)
        {
            builder.ToTable(TableConstants.SYSBIEUMAU_TABLENAME);
            builder.Property(x => x.MaBieuMau).HasField("_maBieuMau").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TenBieuMau).HasField("_tenBieuMau").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TenFile).HasField("_tenFile").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.NoiDung).HasField("_noiDung").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.LoaiFile).HasField("_loaiFile").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.GhiChu).HasField("_ghiChu").HasMaxLength(4000).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CheckSum).HasField("_checkSum").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.GroupName).HasField("_groupName").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsExportPDF).HasField("_isExportPDF").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}