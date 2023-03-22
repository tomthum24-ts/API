using API.INFRASTRUCTURE.DataConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public interface IProjectServices
    {
    }
    public class ProjectServices : IProjectServices
    {
        public readonly DapperContext _context;

        public ProjectServices(DapperContext context)
        {
            _context = context;
        }
    }
}
