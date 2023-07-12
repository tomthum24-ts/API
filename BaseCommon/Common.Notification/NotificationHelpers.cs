using BaseCommon.Common.MethodResult;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
namespace BaseCommon.Common.Notification
{
    public static class NotificationHelpers
    {
        private static ConcurrentDictionary<string, Dictionary<string, string>> _notificationMessages;

        public static string GetNotificationMessage(string messageCode)
        {
            return GetNotificationMessage(messageCode, ref _notificationMessages, typeof(NotificationHelpers).Assembly);
        }

        public static string GetNotificationMessage(string messageCode, ref ConcurrentDictionary<string, Dictionary<string, string>> notificationMessages, Assembly resourceAssembly)
        {
            string defaultNotificationMessage = "No pre-defined notification message";

            Dictionary<string, string> messages = null;

            //var currentLanguage = CultureInfo.DefaultThreadCurrentCulture.TwoLetterISOLanguageName;
            var currentLanguage = "vn";
            var dictionaryKey = $"{resourceAssembly.GetName().Name}@@{currentLanguage}";

            try
            {
                if (notificationMessages != null)
                {
                    messages = notificationMessages[dictionaryKey];
                }
            }
            catch { }

            if (messages == null)
            {
                try
                {
                    string jsonErrorFilePath = $"{Settings.ResourceFolderName}.{Settings.NotificationMessagesFileName}-{currentLanguage}.json";

                    using Stream stream = resourceAssembly.GetManifestResourceStream(resourceAssembly.GetName().Name + '.' + jsonErrorFilePath);

                    using var reader = new StreamReader(stream);

                    var fileData = reader.ReadToEnd();

                    messages = JsonSerializer.Deserialize<Dictionary<string, string>>(fileData);
                }
                catch
                {
                    messages = new Dictionary<string, string>();
                }

                if (messages != null)
                {
                    notificationMessages ??= new ConcurrentDictionary<string, Dictionary<string, string>>();

                    notificationMessages[dictionaryKey] = messages;
                }
            }

            defaultNotificationMessage = messages.Keys.Contains(messageCode) ? messages[messageCode] : defaultNotificationMessage;

            return defaultNotificationMessage;
        }
    }
}