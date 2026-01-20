using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface;

namespace API.INFRASTRUCTURE.Repositories
{
    public class JobsCategoryRepository : RepositoryBase<JobsCategory>, IJobsCategoryRepository
    {
        public JobsCategoryRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
