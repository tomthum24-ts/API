using BaseCommon.Common.Response;

namespace API.APPLICATION.ViewModels.Location
{
    public class VillageRequestViewModel : QueryPaging
    {
        public string Ids { get; set; }
        public string IdProvinces { get; set; }
        public string IdDistricts { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
    }
}
