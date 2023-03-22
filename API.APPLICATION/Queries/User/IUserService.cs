using API.APPLICATION.Parameters.User;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.Response;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE
{
    public interface IUserService
    {

        Task<IEnumerable<UserDTO>> GetAllUser();
        Task<UserDTO> GetInfoUserByID(int id, CancellationToken cancellationToken);
        Task<PagingItems<UserDTO>> GetAllUserPaging(UserFilterParam param);
    }

    public class UserService : IUserService
    {


        public readonly DapperContext _context;


        public UserService(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUser()
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("GetAllUser", new { }, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadAsync<UserDTO>().ConfigureAwait(false);
            return result;
        }
        public async Task<PagingItems<UserDTO>> GetAllUserPaging(UserFilterParam param)
        {
            var result = new PagingItems<UserDTO>
            {
                PagingInfo = new PagingInfoDto
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                }
            };
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("GetAllUser_SelectWithPaging", param, commandType: CommandType.StoredProcedure);
            result.Items = await rs.ReadAsync<UserDTO>().ConfigureAwait(false);
            result.PagingInfo.TotalItems = await rs.ReadSingleAsync<int>().ConfigureAwait(false);
            return result;
        }
        public async Task<UserDTO> GetInfoUserByID(int id, CancellationToken cancellationToken)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("GetUserByID", new { IDUser = id }, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadFirstOrDefaultAsync<UserDTO>();
            return result;
        }

    }
}
