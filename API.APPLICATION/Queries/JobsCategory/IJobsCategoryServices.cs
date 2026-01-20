using API.DOMAIN.DTOs;
using API.DOMAIN.DTOs.Project;
using API.INFRASTRUCTURE.DataConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Queries.JobsCategory
{
    public interface IJobsCategoryServices : IDanhMucQueries<JobsCategoryDTO>
    {
        public class JobsCategoryServices : DanhMucQueries<JobsCategoryDTO>, IJobsCategoryServices
        {
            public JobsCategoryServices(DapperContext context) : base(context)
            {
            }
        }
    }
}
