using API.HRM.DOMAIN.DTOs.User;
using BaseCommon.Common.MethodResult;
using System.Threading;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE
{
    public interface IJWTManagerRepository
    {
        Task<Tokens> GenerateJWTTokens(Users users, CancellationToken cancellationToken);
    }
}
