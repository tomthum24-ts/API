using BaseCommon.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.Notification.Model
{
    /// <summary>
    /// Integration Events notes:
    /// An Event is “something that has happened in the past”, therefore its name has to be past tense
    /// An Integration Event is an event that can cause side effects to other microservices, Bounded-Contexts or external systems.
    /// </summary>
    public class NotificationCreatedIntegrationEvent : NotificationIntegrationEventBase
    {
        private NotificationCreatedIntegrationEvent()
        {
        }

        public NotificationCreatedIntegrationEvent(int userId,
            int? idMap,
            string title,
            string description,
            ENotificationType type,
            int senderId,
            string notes = "",
            bool showPopup = true,
            object metaData = null)
        {
            UserId = userId;
            IdMap = idMap;
            Title = title;
            Description = description;
            Type = type;
            TypeName = Type.GetDescription();
            Notes = notes;
            ShowPopup = showPopup;
            SenderId = senderId;
            if (metaData != null) MetaData = JsonConvert.SerializeObject(metaData);
        }

        public string Title { get; }
        public string Description { get; }
        public ENotificationType Type { get; }
        public string TypeName { get; }
        public string MetaData { get; set; }
        public string Notes { get; }
        public bool ShowPopup { get; set; }
    }
}
