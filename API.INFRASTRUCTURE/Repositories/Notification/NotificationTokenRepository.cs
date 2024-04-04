using API.DOMAIN.DomainObjects.Notification;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.Notification;

namespace API.INFRASTRUCTURE.Repositories.Notification
{
    public class NotificationTokenRepository : RepositoryBase<NotificationToken>, INotificationTokenRepository
    {
        public NotificationTokenRepository(IDbContext db) : base(db)
        {
        }
    }
}