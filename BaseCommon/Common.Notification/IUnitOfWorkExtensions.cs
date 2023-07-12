using BaseCommon.UnitOfWork;
using Hangfire;
using System.Collections.Generic;

namespace BaseCommon.Common.Notification
{
    public static class IUnitOfWorkExtensions
    {
        /// <summary>
        /// Publish the message in the background after the transaction is done successfully.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unitOfWork"></param>
        /// <param name="notification"></param>
        public static void PublishNotification<T>(this IUnitOfWork unitOfWork, IEnumerable<T> notification)
        {
            unitOfWork.ActionsAfterCommit.Add(() =>
            {
                BackgroundJob.Enqueue<NotificationPublisher<T>>(x => x.Publish(notification));
            });
        }
    }
}