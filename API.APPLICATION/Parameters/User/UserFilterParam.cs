using BaseCommon.Common.Response;


namespace API.APPLICATION.Parameters.User
{
    public class UserFilterParam 
    {
        public string Keyword { get; set; }
        public string SortName { get; set; }
        public bool SortASC { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int? id { get; set; }
    }
}
