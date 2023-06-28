using System.Collections.Generic;

namespace BaseCommon.Model
{
    public class PermissionGroupCache
    {
        private const string CacheName = "PermissionGroupCacheItem";
        public int IdGroupPermission { get; set; }
        public List<string> ListOfPermission { get; set; }
        public override string ToString()
        {
            return CacheName;
        }
    }
}
