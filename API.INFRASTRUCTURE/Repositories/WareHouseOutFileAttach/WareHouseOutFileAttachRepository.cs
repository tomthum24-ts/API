using API.DOMAIN.DomainObjects.WareHouseInFileAttach;
using API.DOMAIN.DomainObjects.WareHouseOutFileAttach;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface;

namespace API.INFRASTRUCTURE.Repositories
{
    public class WareHouseOutFileAttachRepository : RepositoryBase<WareHouseOutFileAttachs>, IWareHouseOutFileAttachRepository
    {
        public WareHouseOutFileAttachRepository(IDbContext db) : base(db)
        {
        }
    }
}
