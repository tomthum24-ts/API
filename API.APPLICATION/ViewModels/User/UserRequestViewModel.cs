using BaseCommon.Common.Response;

namespace API.APPLICATION.ViewModels.User
{
    public class UserRequestViewModel : QueryPaging
    {
        public int? IDUser { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }

    }
}
