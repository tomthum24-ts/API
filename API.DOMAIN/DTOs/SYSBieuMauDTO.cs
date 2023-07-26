namespace API.DOMAIN.DTOs
{
    public class SYSBieuMauDTO
    {
        public int Id { get; set; }
        public string MaBieuMau { get; set; }
        public string TenBieuMau { get; set; }
        public string TenFile { get; set; }
        public byte[] NoiDung { get; set; }
        public string LoaiFile { get; set; }
        public string GhiChu { get; set; }
        public string CheckSum { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public string GroupName { get; set; }
        public bool IsExportPDF { get; set; }
    }
}