using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.Notification
{
    public class CustomNotificationModel
    {
        public string FireBaseToken { get; set; }
        public CustomNotification Notification { get; set; }=new CustomNotification();
        public CustomDataDetail Data { get; set; }
    }
    public class CustomNotification
    {
        public string Title { get; set; } 
        public string Body { get; set; } 

    }
    public class CustomDataDetail
    {
        public string Priority { get; set; } 
        public CustomURL URL { get; set; }
    }
    public class CustomURL
    {
        public int ModuleType { get; set; } 
        public string ModuleName { get; set; } 
        public int Id { get; set; } = 1;
    }
}
