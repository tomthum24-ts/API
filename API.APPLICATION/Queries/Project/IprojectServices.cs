using API.INFRASTRUCTURE.DataConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.Project
{
    public interface IprojectServices
    {
    }
    public class ProjectServices : IprojectServices
    {
        public readonly DapperContext _context;

        public ProjectServices(DapperContext context)
        {
            _context = context;
        }
    }
}
