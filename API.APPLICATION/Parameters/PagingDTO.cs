
namespace API.APPLICATION.Parameters
{
    public class PagingDTO
    {
        public string Keyword { get; set; }
        public string SortCol { get; set; }
        public bool IsAsc { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
