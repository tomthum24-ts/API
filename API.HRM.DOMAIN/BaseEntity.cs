using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.HRM.DOMAIN
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? DeletedById { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}
