using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DOMAIN.DTOs.Menu
{

    public class MenuCapMotDTO
    {
        public int? C1_IdParent { get; set; }
        public int? C1_Id { get; set; }
        public string C1_Name { get; set; }
        public string C1_Link { get; set; }
        public string C1_Image { get; set; }
        public IEnumerable<MenuCapHaiDTO> MenuCapHais { get; set; }
    }
    public class MenuCapHaiDTO
    {
        public int? C2_IdParent { get; set; }
        public int? C2_Id { get; set; }
        public string C2_Name { get; set; }
        public string C2_Link { get; set; }
        public string C2_Image { get; set; }
        public IEnumerable<MenuCapBaDTO> MenuCapBas { get; set; }
    }
    public class MenuCapBaDTO
    {
        public int? C3_IdParent { get; set; }
        public int? C3_Id { get; set; }
        public string C3_Name { get; set; }
        public string C3_Link { get; set; }
        public string C3_Image { get; set; }
    }

}
