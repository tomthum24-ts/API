using API.DOMAIN;
using API.DOMAIN.DomainObjects.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.INFRASTRUCTURE.EFConfigs.Notification
{
    public class NotificationTokenConfiguration : IEntityTypeConfiguration<NotificationToken>
    {
        public void Configure(EntityTypeBuilder<NotificationToken> builder)
        {
            builder.ToTable(TableConstants.NOTIFICATIONTOKEN_TABLENAME);
            builder.Property(x => x.TokenFireBase).HasField("_tokenFireBase").HasColumnType("nvarchar(max)").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.UserId).HasField("_userId").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.UserName).HasField("_userName").HasMaxLength(50).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.DeviceId).HasField("_deviceId").HasMaxLength(200).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Property(x => x.Note).HasField("_note").HasMaxLength(500).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}