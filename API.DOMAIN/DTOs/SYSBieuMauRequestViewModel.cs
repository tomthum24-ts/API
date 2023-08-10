using System.Text.Json.Serialization;

namespace API.DOMAIN.DTOs
{
    public class SYSBieuMauRequestViewModel
    {
        public string MaBieuMau { get; set; }

        [JsonIgnore]
        public int? IdNhanSuLogin { get; set; }

        [JsonIgnore]
        public int? Type { get; set; }

        [JsonIgnore]
        public int? ManHinh { get; set; }
    }
}