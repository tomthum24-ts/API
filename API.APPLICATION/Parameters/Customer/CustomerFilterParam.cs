namespace API.APPLICATION.Parameters.Customer
{
    public class CustomerFilterParam : PagingDTO
    {
        public string Ids { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Villages { get; set; }
        public string MemberGroups { get; set; }
    }
}