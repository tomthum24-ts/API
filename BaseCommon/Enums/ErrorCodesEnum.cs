using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Enums
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
