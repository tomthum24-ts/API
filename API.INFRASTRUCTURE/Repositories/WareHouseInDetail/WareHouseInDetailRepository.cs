using API.DOMAIN.DomainObjects.WareHouseInDetail;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Repositories;

namespace API.INFRASTRUCTURE
{
    public class WareHouseInDetailRepository : RepositoryBase<WareHouseInDetail>, IWareHouseInDetailRepository
    {
        public WareHouseInDetailRepository(IDbContext db) : base(db)
        {
        }
    }
}