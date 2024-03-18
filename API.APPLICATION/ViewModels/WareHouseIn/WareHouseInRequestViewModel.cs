using BaseCommon.Common.Response;
using Newtonsoft.Json;
using System;

namespace API.APPLICATION.ViewModels.WareHouseIn
{
    public class WareHouseInRequestViewModel : QueryPaging
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}