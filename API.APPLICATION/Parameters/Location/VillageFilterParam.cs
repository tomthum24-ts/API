
namespace API.APPLICATION.Parameters.Location
{
    public class VillageFilterParam : PagingDTO
    {
        public string Ids { get; set; }
        public string IdProvinces { get; set; }
        public string IdDistricts { get; set; }
    }
}
