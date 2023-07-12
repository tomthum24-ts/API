using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.Notification.Model
{
    public class NotificationDeletedIntegrationEvent : NotificationIntegrationEventBase
    {
        private NotificationDeletedIntegrationEvent()
        {
        }

        public NotificationDeletedIntegrationEvent(int userId, int? idMap, int senderId)
        {
            UserId = userId;
            IdMap = idMap;
            SenderId = senderId;
        }
    }
}
