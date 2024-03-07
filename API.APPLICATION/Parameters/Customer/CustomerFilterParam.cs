namespace API.APPLICATION.Parameters.Customer
{
    public class CustomerFilterParam : PagingDTO
    {
        public string Ids { get; set; }
        public string Provinces { get; set; }
        public string Distrist { get; set; }
        public string Village { get; set; }
        public string MemberGroup { get; set; }
    }
}