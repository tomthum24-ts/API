using BaseCommon.Common.Response;

namespace API.APPLICATION.ViewModels.Location
{
    public class DistrictRequestViewModel : QueryPaging
    {
        public string Id { get; set; }
        public string IdProvince { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
    }
    public class DistrictRequestViewModelById
    {
        public int? Id { get; set; }
    }
}
