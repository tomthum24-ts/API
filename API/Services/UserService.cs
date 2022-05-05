
using API.Data;
using API.DomainObjects;
using API.DTOs;
using API.Helpers;
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
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetInfoUserByID(int id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
       
        public readonly DapperContext _context;


        public UserService(IOptions<AppSettings> appSettings, DapperContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest param)
        {
            var getAllUser = GetAll();

            var user = getAllUser.Result.SingleOrDefault(x => x.UserName == param.Username && x.PassWord == param.Password);
            UserViewModel model = new UserViewModel();
            // return null if user not found
            if (user == null) return null;
            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
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

        // helper methods

        private string generateJwtToken(UserDTO user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()), new Claim("UserName", user.UserName.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
