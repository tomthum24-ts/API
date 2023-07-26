using System.Collections.Generic;
using System.Linq;

namespace BaseCommon.Common.Report.Models
{
    public class RequestExcelSimpleReport
    {
        public List<object> DataSource { get; set; }
        public string MaBieuMau { get; set; }
        public Dictionary<string, string> ReplaceSameValues { get; set; }
        public List<string> ColumnDelete { get; set; }
        public int PositionOfSheet { get; set; }
    }

    // Null DataSource

    public class RequestExcelGroupDataReport
    {
        public List<IGrouping<string, object>> GroupData { get; set; }
        public string MaBieuMau { get; set; }
        public string GroupBox { get; set; }
        public string GroupName { get; set; }
        public Dictionary<string, string> ReplaceSameValues { get; set; } = new Dictionary<string, string>();
        public int PositionOfSheet { get; set; }

        public List<RequestExcelDataInGroup> DataInGroups { get; set; }
    }

    public class RequestExcelDataInGroup
    {
        public string GroupName { get; set; }
        public Dictionary<string, string> DictValueGroupName { get; set; } = new Dictionary<string, string>();
    }

    public class RequestExcelGroupTwoLevelDataReport
    {
        public List<Dictionary<string, List<Dictionary<string, List<object>>>>> DataSource { get; set; }
        public string MaBieuMau { get; set; }
        public Dictionary<string, string> ReplaceSameValues { get; set; } = new Dictionary<string, string>();
        public List<string> ColumnDelete { get; set; }
        public string GroupBox1 { get; set; }
        public string GroupBox2 { get; set; }
        public string GroupName1 { get; set; }
        public string GroupName2 { get; set; }
        public bool IsFindAll { get; set; }
        public int PositionOfSheet { get; set; }
        public List<RequestExcelDataInGroup> DataInGroups { get; set; }
    }

    public class RequestGroupTableWordReport
    {
        public List<object> GroupData { get; set; }
        public string GroupName { get; set; }
    }
}