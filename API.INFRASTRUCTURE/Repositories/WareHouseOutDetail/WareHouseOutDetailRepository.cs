using API.DOMAIN.DomainObjects.WareHouseOutDetail;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Repositories;

namespace API.INFRASTRUCTURE
{
    public class WareHouseOutDetailRepository : RepositoryBase<WareHouseOutDetail>, IWareHouseOutDetailRepository
    {
        public WareHouseOutDetailRepository(IDbContext db) : base(db)
        {
        }
    }
}