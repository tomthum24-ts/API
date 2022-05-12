using System;

namespace API.InterFace
{
    public class BaseEntity
    {
        public DateTime? DeletionDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}
