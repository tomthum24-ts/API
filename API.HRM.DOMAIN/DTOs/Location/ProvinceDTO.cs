
using System;

namespace API.DOMAIN.DTOs.Location
{
    public class ProvinceDTO
    {
        public int? STT { get; set; }
        public int Id { get; set; }
        public string ProvinceName { get; set; }
        public string CodeName { get; set; }
        public string DivisionType { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
        public string NguoiTao { get; set; }
        public DateTime? CreationDate { get; set; }
        public string NguoiSua { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
