using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.Media
{
    public class MediaRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? type { get; set; }
        public double? Size { get; set; }
        public string Path { get; set; }
        public bool? ForWeb { get; set; }
        public string CheckSum { get; set; }
    }
}
