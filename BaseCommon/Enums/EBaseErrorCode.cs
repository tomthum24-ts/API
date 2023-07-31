using System.ComponentModel;

namespace BaseCommon.Enums
{
    public enum EBaseErrorCode
    {
        [Description("Dữ liệu đã tồn tại trong hệ thống")]
        EB01,

        [Description("Không tìm thấy dữ liệu cập nhật")]
        EB02
    }
}