using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Entities
{
    public abstract class Entity<TKey> : BaseEntity where TKey : struct
    {
        public TKey Id { get; set; }

        public TKey? CreatedById { get; set; }

        public TKey? UpdatedById { get; set; }

        public TKey? DeletedById { get; set; }
    }
}
