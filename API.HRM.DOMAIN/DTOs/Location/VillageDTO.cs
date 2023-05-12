using System;

namespace API.DOMAIN.DTOs.Location
{
    public class VillageDTO
    {
        public int Id { get; set; }
        public int? IdProvince { get; set; }
        public string ProvinceName { get; set; }
        public int? IdDistrict { get; set; }
        public string DistrictName { get; set; }
        public string CodeName { get; set; }
        public string VillageName { get; set; }
        public string DivisionType { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? CreationDate { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
