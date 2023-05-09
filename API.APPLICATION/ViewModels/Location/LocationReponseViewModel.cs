using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.Location
{
    public class LocationReponseViewModel 
    {
        public string name { get; set; }
        public int code { get; set; }
        public string codename { get; set; }
        public string division_type { get; set; }
        public int phone_code { get; set; }
        public List<DistrictViewModel> districts { get; set; }

    }
    public class DistrictViewModel
    {
        public string name { get; set; }
        public int code { get; set; }
        public string codename { get; set; }
        public string division_type { get; set; }
        public string short_codename { get; set; }
        public List<WardViewModel> wards { get; set; }
    }

    public class WardViewModel
    {
        public string name { get; set; }
        public int code { get; set; }
        public string codename { get; set; }
        public string division_type { get; set; }
        public string short_codename { get; set; }
    }
}
