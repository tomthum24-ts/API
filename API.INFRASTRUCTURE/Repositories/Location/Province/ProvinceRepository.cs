using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.Location;

namespace API.INFRASTRUCTURE.Repositories
{
    public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
    {
        public ProvinceRepository(IDbContext db) : base(db)
        {
        }
    }
}