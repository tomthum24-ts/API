using API.DOMAIN.DomainObjects.BieuMau;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.BieuMau;

namespace API.INFRASTRUCTURE.Repositories.BieuMau
{
    public class SysBieuMauRepository : RepositoryBase<SysBieuMau>, ISysBieuMauRepository
    {
        public SysBieuMauRepository(IDbContext db) : base(db)
        {
        }
    }
}