using BaseCommon.Common.Response;
using System;

namespace API.APPLICATION.ViewModels.Customer
{
    public class CustomerRequestViewModel : QueryPaging
    {
        public string Ids { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Villages { get; set; }
        public string MemberGroups { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }

    }
}