using BaseCommon.Common.Response;

namespace API.APPLICATION.ViewModels.Location
{
    public class VillageRequestViewModel : QueryPaging
    {
        public string Id { get; set; }
        public string IdProvince { get; set; }
        public string IdDistrict { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
    }
}
