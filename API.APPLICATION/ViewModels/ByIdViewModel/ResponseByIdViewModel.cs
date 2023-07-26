using BaseCommon.Common.Report.Infrastructures;
using System.Net;
using System.Text.RegularExpressions;

namespace API.APPLICATION.ViewModels.ByIdViewModel
{
    public class ResponseByIdViewModel
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