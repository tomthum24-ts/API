using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE
{
    public class JobApplysRepository : RepositoryBase<JobApplys>, IJobApplysRepository
    {
        public JobApplysRepository(IDbContext db) : base(db)
        {
        }
    }
}
