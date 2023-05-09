using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Infrastructures
{
    public class WordReportHelper
    {
        public const string MailMegreHTMLPrefix = "HTML_";
        public class WordTemplateTable
        {
            public HashSet<string> ColumnKeyWord { get; set; }
            public List<object> DataTable { get; set; }
            public string Prefix { get; set; }
            public bool IsDeleteRows { get; set; }
            public bool IsFormatHTML { get; set; }
        }
    }
}
