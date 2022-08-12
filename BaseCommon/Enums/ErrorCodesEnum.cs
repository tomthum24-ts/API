using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Enums
{

    public enum EErrorCode
    {
        [Description("Dữ liệu đã tồn tại trong hệ thống")]
        EB01,

        [Description("Không tìm thấy dữ liệu trong hệ thống")]
        EB02,
        [Description("Mật khẩu yếu")]
        EB03

    }

}
