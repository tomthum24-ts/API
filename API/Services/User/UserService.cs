using API.Data;

using API.DTOs;
using API.Models;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IUserService
    {

        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetInfoUserByID(int id);
    }

    public class UserService : IUserService
    {

       
        public readonly DapperContext _context;


        public UserService( DapperContext context)
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
