using API.DOMAIN.DomainObjects.WareHouseOut;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Repositories;

namespace API.INFRASTRUCTURE
{
    public class WareHouseOutRepository : RepositoryBase<WareHouseOut>, IWareHouseOutRepository
    {
        public WareHouseOutRepository(IDbContext db) : base(db)
        {
        }
    }
}