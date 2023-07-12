using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.Notification.Model
{
    public abstract class NotificationIntegrationEventBase
    {
        public int UserId { get; set; }
        public int? IdMap { get; set; }
        public int SenderId { get; set; }
    }
}
