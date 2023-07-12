using BaseCommon.UnitOfWork;
using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.Notification
{
    public static class ICapPublisherExtensions
    {
        /// <summary>
        /// Publish the message after the transaction is done successfully.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="publisher"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="notification"></param>
        public static void PublishNotification<T>(this ICapPublisher publisher, IUnitOfWork unitOfWork, IEnumerable<T> notification)
        {
            unitOfWork.ActionsAfterCommit.Add(() =>
            {
                publisher.PublishNotification(notification);
            });
        }

        /// <summary>
        /// Publish message immediately.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="publisher"></param>
        /// <param name="notification"></param>
        public static void PublishNotification<T>(this ICapPublisher publisher, IEnumerable<T> notification)
        {
            if (notification != null && notification.Any())
            {
                foreach (var item in notification)
                {
                    publisher.Publish(item.GetType().Name, contentObj: item);
                }
            }
        }
    }
}
