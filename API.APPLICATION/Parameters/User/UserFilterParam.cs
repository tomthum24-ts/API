using BaseCommon.Common.Response;


namespace API.APPLICATION.Parameters.User
{
    public class UserFilterParam 
    {
        public int? Id { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        
    }
}
