using API.HRM.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE.DataConnect;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Services.User
{
    public interface IUserService
    {

        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetInfoUserByID(int id);
    }

    public class UserService : IUserService
    {


        public readonly DapperContext _context;


        public UserService(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("GetAllUser", new { }, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadAsync<UserDTO>().ConfigureAwait(false);
            return result;
        }
        public async Task<UserDTO> GetInfoUserByID(int id)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("GetUserByID", new { IDUser = id }, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadFirstOrDefaultAsync<UserDTO>();
            return result;
        }

    }
}
