
using BaseCommon.Common.Response;

namespace API.APPLICATION.ViewModels.BaseClasses
{
    public class DanhMucRequestViewModel : QueryPaging
    {
        public string FindId { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool SortByASC { get; set; }
    }
}
