using API.DOMAIN.DomainObjects.WareHouseInFileAttach;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface;

namespace API.INFRASTRUCTURE.Repositories
{
    public class WareHouseInFileAttachRepository : RepositoryBase<WareHouseInFileAttachs>, IWareHouseInFileAttachRepository
    {
        public WareHouseInFileAttachRepository(IDbContext db) : base(db)
        {
        }
    }
}