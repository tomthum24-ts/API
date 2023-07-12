using DotNetCore.CAP;
using System.Collections.Generic;
using System.Linq;

namespace BaseCommon.Common.Notification
{
    public class NotificationPublisher<T>
    {
        private ICapPublisher _publisher;

        public NotificationPublisher(ICapPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Publish(IEnumerable<T> notification)
        {
            if (notification != null && notification.Any())
            {
                foreach (var item in notification)
                {
                    _publisher.Publish(item.GetType().Name, contentObj: item);
                }
            }
        }
    }
}