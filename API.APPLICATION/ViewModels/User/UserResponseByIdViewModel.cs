using BaseCommon.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.User
{
    public class UserResponseByIdViewModel
    {
        private string _objValue = string.Empty;
        public int Id { get; set; }
        public string ObjKey { get; set; }

        public string ObjValue
        {
            get
            {
                return _objValue;
            }
            set
            {
                if (!ObjKey.StartsWith(WordReportHelper.MailMegreHTMLPrefix))
                {
                    _objValue = Regex.Replace(WebUtility.HtmlDecode(value), "<.*?>", string.Empty);
                }
                else
                {
                    _objValue = value;
                }
            }
        }

        //
    }

    public class ReportReplaceInfoDTO
    {
        public int Id { get; set; }
        public string ObjKey { get; set; }
        public string ObjValue { get; set; }
    }
}

