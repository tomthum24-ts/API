using API.APPLICATION.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Parameters.JobApplys
{
    public class JobApplysFilterParam : PagingDTO
    {
        public string Ids { get; set; }
    }
}
