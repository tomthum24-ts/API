using API.HRM.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableConstants.User_TABLENAME);
            builder.Property(x => x.UserName).HasField("_userName").HasMaxLength(225).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.PassWord).HasField("_passWord").HasMaxLength(40).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(255).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.LastName).HasField("_lastName").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Email).HasField("_email").HasMaxLength(255).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Address).HasField("_address").HasMaxLength(255).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Phone).HasField("_phone").HasMaxLength(15).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Department).HasField("_department").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.BirthDay).HasField("_birthDay").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Province).HasField("_province").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.District).HasField("_district").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Village).HasField("_village").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Project).HasField("_project").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasMaxLength(500).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Status).HasField("_status").UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
