using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.Response
{
    public interface ITotalCount
    {
        /// <summary>
        /// Total count of Item.
        /// </summary>
        int TotalItems { get; set; }
    }
}
