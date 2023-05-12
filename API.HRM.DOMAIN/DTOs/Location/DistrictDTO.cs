using System;
namespace API.DOMAIN.DTOs.Location
{
    public class DistrictDTO
    {
        public int Id { get; set; }
        public int? IdProvince { get; set; }
        public string ProvinceName { get; set; }
        public string CodeName { get; set; }
        public string DistrictName { get; set; }
        public string DivisionType { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
        public string UserCreate { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
