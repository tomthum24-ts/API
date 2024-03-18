using BaseCommon.Common.Response;
using System;

namespace API.APPLICATION.ViewModels.WareHouseOut
{
    public class WareHouseOutRequestViewModel : QueryPaging
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}