using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.Response
{
    public class PagingInfoDto : ITotalCount, IPagedRequest
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
