using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Caching.Interfaces
{
    public interface IDistributedCacheKeyNormalizer
    {
        string NormalizeKey(DistributedCacheKeyNormalizeArgs args);
    }
}
