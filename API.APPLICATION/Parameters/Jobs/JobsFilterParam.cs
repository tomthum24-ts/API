using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Parameters.Jobs
{
    public class JobsFilterParam : PagingDTO
    {
        public string Ids { get; set; }
        public string Provinces { get; set; }
        public string Districts { get; set; }
        public string Villages { get; set; }
    }
}
