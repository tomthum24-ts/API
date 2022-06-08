using BaseCommon.Common.Response;

namespace API.APPLICATION.ViewModels.User
{
    public class UserRequestViewModel : QueryPaging
    {
        public int? Id { get; set; }
        public string Keyword { get; set; }
        public string SortName { get; set; }
        public bool SortASC { get; set; }

    }
}
