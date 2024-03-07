using API.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(TableConstants.CUSTOMER_TABLENAME);
            builder.Property(x => x.Code).HasField("_code").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Address).HasField("_address").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Province).HasField("_province").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.District).HasField("_district").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Village).HasField("_village").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Phone).HasField("_phone").HasMaxLength(20).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Phone2).HasField("_phone2").HasMaxLength(20).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.CMND).HasField("_cMND").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Birthday).HasField("_birthday").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Email).HasField("_email").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.TaxCode).HasField("_taxCode").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.GroupMember).HasField("_groupMember").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.FileAttach).HasField("_fileAttach").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.IsEnterprise).HasField("_isEnterprise").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.EnterpriseName).HasField("_enterpriseName").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Representative).HasField("_representative").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Poisition).HasField("_poisition").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.RegistrationNumber).HasField("_registrationNumber").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.RegistrationDate).HasField("_registrationDate").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.RegistrationAddress).HasField("_registrationAddress").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}