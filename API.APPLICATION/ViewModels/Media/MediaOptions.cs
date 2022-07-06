using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.Media
{
    public class MediaOptions
    {
        public string MediaUploadUrl { get; set; }
        public string MediaUrl { get; set; }
        public string FolderForWeb { get; set; }
        public string[] PermittedExtensions { get; set; }
        public long SizeLimit { get; set; }
    }
}
