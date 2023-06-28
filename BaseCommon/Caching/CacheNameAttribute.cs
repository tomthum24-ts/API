using BaseCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Caching
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class CacheNameAttribute : Attribute
    {
        public string Name { get; set; }

        public CacheNameAttribute([NotNull] string name)
        {
            Name = name;
        }

        public static string GetCacheName(Type cacheItemType)
        {
            var cacheNameAttribute = cacheItemType
                .GetCustomAttributes(true)
                .OfType<CacheNameAttribute>()
                .FirstOrDefault();

            if (cacheNameAttribute != null)
            {
                return cacheNameAttribute.Name;
            }

            return cacheItemType.FullName.RemovePostFix("CacheItem");
        }
    }
}
