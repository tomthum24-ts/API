using BaseCommon.Caching;
using System.Collections.Generic;

namespace BaseCommon.Authorization.Model
{
    [CacheName("UserItemCache")]
    public class UserItemCache
    {
        public int UserId { get; set; }
        public bool IsPermissionChanged { get; set; }
        public string ListOfPermission { get; set; }
        public List<string> ListOfSessionCodeInValid { get; set; }
    }
}
