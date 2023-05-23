using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.RefreshTooken;

namespace API.INFRASTRUCTURE.Repositories
{
    public class RefreshTookenRepository : RepositoryBase<RefreshToken>, IRefreshTookenRepository
    {
        public RefreshTookenRepository(IDbContext db) : base(db)
        {
        }
    }
}
