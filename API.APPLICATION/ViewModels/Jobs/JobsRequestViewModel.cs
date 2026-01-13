using BaseCommon.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.Jobs
{
    public class JobsRequestViewModel : QueryPaging
    {
        public string Ids { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Villages { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
    }
}
