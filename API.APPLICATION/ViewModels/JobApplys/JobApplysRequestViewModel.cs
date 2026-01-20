using BaseCommon.Common.Response;

namespace API.APPLICATION.ViewModels.JobApplys
{
    public class JobApplysRequestViewModel : QueryPaging
    {
        public string Ids { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
    }
}
