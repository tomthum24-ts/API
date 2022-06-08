using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.Response
{
    public class PagingItems<T> where T : class
    {
        public PagingItems()
        {
        }

        public PagingItems(int pageSize, int pageNumber, int totalItems)
        {
            PagingInfo = new PagingInfoDto
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalItems = totalItems
            };
        }

        public IEnumerable<T> Items { get; set; }
        public PagingInfoDto PagingInfo { get; set; }
    }
}
