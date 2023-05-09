using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.User
{
    public class UserRequestByIdViewModel
    {
        public int LoaiThongTin { get; set; }
        public int IDKhoaChinh { get; set; }
        public string StrKeyFiter { get; set; }
    }
}
