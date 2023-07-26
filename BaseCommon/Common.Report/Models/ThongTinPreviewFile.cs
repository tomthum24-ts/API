using System.Collections.Generic;

namespace BaseCommon.Common.Report.Models
{
    public class PreviewRequestDto : IPreviewRequest
    {
        public bool IsPreview { get; set; }
    }

    public class ThongTinPreviewFile
    {
        public List<string> Images { get; set; } = new List<string>();
        public int TotalPage { get; set; }
    }
}