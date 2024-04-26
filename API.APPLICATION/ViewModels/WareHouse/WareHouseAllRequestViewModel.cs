using BaseCommon.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.WareHouse
{
    public class WareHouseAllRequestViewModel : QueryPaging
    {
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
    }
}
