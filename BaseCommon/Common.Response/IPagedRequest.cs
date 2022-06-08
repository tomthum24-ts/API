using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.Response
{
    public interface IPagedRequest
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
}
