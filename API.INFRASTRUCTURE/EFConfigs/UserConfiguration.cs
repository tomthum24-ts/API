using API.HRM.DOMAIN;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.Property(x => x.id).HasField("_id").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.UserName).HasField("_userName").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.PassWord).HasField("_password").HasMaxLength(32).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Name).HasField("_name").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Address).HasField("_address").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Phone).HasField("_phone").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.status).HasField("_status").UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
