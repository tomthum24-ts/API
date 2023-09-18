using API.DOMAIN;
using API.DOMAIN.DTOs.User;
using BaseCommon.Common.MethodResult;
using System.Threading;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE
{
    public interface IJWTManagerRepository
    {
        Task<Tokens> GenerateJWTTokens(Users users, CancellationToken cancellationToken);
        UserRefreshToken GenerateRefreshToken(string ipAddress, string userName);
    }
}
