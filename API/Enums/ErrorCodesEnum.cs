using System.ComponentModel;

namespace API.Enums
{
    public class ErrorCodesEnum
    {
        public enum EBaseErrorCode
        {
            EB01,
            [Description("Không tìm thấy dữ liệu cập nhật")]
            EB02
        }
    }
}
