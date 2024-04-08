using BaseCommon.Common.Response;


namespace API.APPLICATION.Parameters.User
{
    public class UserFilterParam : PagingDTO
    {
        public string Ids { get; set; }
        public bool IsAdmin { get; set; }

    }
}
