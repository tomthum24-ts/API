﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
