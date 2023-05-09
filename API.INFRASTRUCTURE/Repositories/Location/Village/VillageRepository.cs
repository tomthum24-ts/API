
using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.Location;

namespace API.INFRASTRUCTURE.Repositories
{
    public class VillageRepository : RepositoryBase<Village>, IVillageRepository
    {
        public VillageRepository(IDbContext db) : base(db)
        {
        }
    }
}
