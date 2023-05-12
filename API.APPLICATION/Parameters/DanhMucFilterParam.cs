
namespace API.APPLICATION
{
    public class DanhMucFilterParam
    {
        public string FindId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string TableName { get; set; }
        public string ColumName { get; set; }
        public string SortCol { get; set; }
        public bool SortByASC { get; set; }
        public string KeyWord { get; set; }
    }
}
