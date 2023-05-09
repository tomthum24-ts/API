using BaseCommon.Common.MethodResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Entities
{
    public abstract class BaseEntity : EntityValidator
    {
        protected BaseEntity()
        {
            _errorMessages ??= new List<ErrorResult>();

            _warningResults ??= new List<WarningResult>();

        }
        public DateTime? DeletionDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}

