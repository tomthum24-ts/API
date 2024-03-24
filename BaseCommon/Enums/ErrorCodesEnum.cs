using System.ComponentModel;

namespace BaseCommon.Enums
{
    public enum EErrorCode
    {
        [Description("Dữ liệu đã tồn tại trong hệ thống")]
        EB01,

        [Description("Không tìm thấy dữ liệu trong hệ thống")]
        EB02,

        [Description("Mật khẩu yếu")]
        EB03,

        [Description("Hết hạn phiên đăng nhập")]
        EB04,

        [Description("Hết phiên đăng nhập vui lòng đăng nhập lại")]
        EB05,

        [Description("Kiểm tra lại file upload")]
        EB06,
        [Description("Không được để trống chi tiết")]
        EB07
    }
    public enum ESysBieuMauErrorCode
    {
        SBMC01,
        SBMC02,
        SBMC03,
        SBMC04,
        SBMC05,
        SBMC06,
        SBMC07,
        SBMC08,
        SBMC09,
    }
}