
namespace API.APPLICATION.Parameters.Location
{
    public class VillageFilterParam : PagingDTO
    {
        public string Id { get; set; }
        public string IdProvince { get; set; }
        public string IdDistrict { get; set; }
    }
}
