
using System.Collections.Generic;

namespace API.DOMAIN.DTOs.Menu
{
    public class MenuRank
    {
        public IEnumerable<MenuDTO> MenuRankDTO { get; set; }
    }
    public class MenuDTO
    {
        public int Id { get; set; }
        public int? ParentID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
}
