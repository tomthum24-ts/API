using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.Base64
{
    public class Base64Model
    {
        public string ContentType { get; set; }
        public string DataStream { get; set; }
        public string FileName { get; set; }
    }
}
