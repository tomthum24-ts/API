using System.Text.Json.Serialization;

namespace API.DOMAIN
{
    public class ReportReplaceInfoRequestViewModel
    {
        public int LoaiThongTin { get; set; }
        public int IDKhoaChinh { get; set; }
        public string StrKeyFiter { get; set; }
    }
}