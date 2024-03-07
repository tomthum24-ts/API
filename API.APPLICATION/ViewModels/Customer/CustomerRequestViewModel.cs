using BaseCommon.Common.Response;
using System;

namespace API.APPLICATION.ViewModels.Customer
{
    public class CustomerRequestViewModel : QueryPaging
    {
        public string Ids { get; set; }
        public string Provinces { get; set; }
        public string Distrist { get; set; }
        public string Village { get; set; }
        public string MemberGroup { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }

    }
}