using BaseCommon.Common.Response;

namespace API.APPLICATION.Parameters.WareHouse
{
    public class WareHouseAllFilterParam : QueryPaging
    {
        public int IdUser { get; set; }
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
    }
}