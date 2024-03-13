using API.DOMAIN;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Repositories;

namespace API.INFRASTRUCTUREm
{
    public class WareHouseInRepository : RepositoryBase<WareHouseIn>, IWareHouseInRepository
    {
        public WareHouseInRepository(IDbContext db) : base(db)
        {
        }
    }
}