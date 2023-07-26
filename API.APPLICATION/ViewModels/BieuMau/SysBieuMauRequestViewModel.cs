using System.Text.Json.Serialization;

namespace API.APPLICATION.ViewModels.BieuMau
{
    public class SysBieuMauRequestViewModel
    {
        public string MaBieuMau { get; set; }

        [JsonIgnore]
        public int? IdNhanSuLogin { get; set; }

        [JsonIgnore]
        public int? Type { get; set; }

        [JsonIgnore]
        public int? ManHinh { get; set; }
    }

    public class SysBieuMauPagingRequestViewModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortName { get; set; }
        public bool SortASC { get; set; }
        public string Keyword { get; set; }

        [JsonIgnore]
        public int? IdNhanSuLogin { get; set; }

        [JsonIgnore]
        public int? Type { get; set; }

        [JsonIgnore]
        public int? ManHinh { get; set; }
    }
}