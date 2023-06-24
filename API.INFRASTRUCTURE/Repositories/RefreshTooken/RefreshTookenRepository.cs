using API.DOMAIN;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.RefreshToken;

namespace API.INFRASTRUCTURE.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<UserRefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IDbContext db) : base(db)
        {
        }
    }
}
