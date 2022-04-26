using API.Data;
using API.Models;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using API.DTOs;
using API.InterFace.User;

namespace API.Queries
{
    public class UserQueries : IUserService
    {
        public readonly DapperContext _context;

        public UserQueries(DapperContext context)
        {
            _context = context;
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
