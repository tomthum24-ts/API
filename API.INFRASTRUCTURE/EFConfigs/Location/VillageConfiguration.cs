using API.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace API.INFRASTRUCTURE.EFConfigs.Location
{
    internal class VillageConfiguration : IEntityTypeConfiguration<Village>
    {
        public void Configure(EntityTypeBuilder<Village> builder)
        {
            builder.ToTable(TableConstants.VILLAGE_TABLENAME);
            builder.Property(x => x.VillageCode).HasField("_villageCode").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.VillageName).HasField("_villageName").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CodeName).HasField("_codeName").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.DivisionType).HasField("_divisionType").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IdDistrict).HasField("_idDistrict").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasMaxLength(1).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Status).HasField("_status").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
