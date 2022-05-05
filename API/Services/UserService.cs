
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
        IEnumerable<UserViewModel> GetAll();
        Task<UserDTO> GetInfoUserByID(int id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
       
        public readonly DapperContext _context;
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserViewModel> _users = new List<UserViewModel>
        {
            new UserViewModel { ID = 1,  Name = "User", UserName = "test", Password = "test" }
        };



        public UserService(IOptions<AppSettings> appSettings, DapperContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {

            var user = _users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            return _users;
        }
        public async Task<UserDTO> GetInfoUserByID(int id)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("GetUserByID", new { IDUser = id }, commandType: CommandType.StoredProcedure);
            var result = await rs.ReadFirstOrDefaultAsync<UserDTO>();
            return result;
        }

        // helper methods

        private string generateJwtToken(UserViewModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var a= tokenDescriptor.Subject.ToString();
            var b = tokenDescriptor.SigningCredentials.ToString();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
