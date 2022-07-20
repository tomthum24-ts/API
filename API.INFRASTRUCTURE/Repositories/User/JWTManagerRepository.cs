using API.HRM.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.Enum;
using BaseCommon.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Repositories.User
{
	public class JWTManagerRepository : IJWTManagerRepository
	{

		private readonly IConfiguration _iconfiguration;

		private readonly DapperContext _context;
        public JWTManagerRepository(IConfiguration iconfiguration, DapperContext context)
        {
            _iconfiguration = iconfiguration;
            _context = context;
        }
        public Tokens GenerateJWTTokens(Users users)
		{
			var user = GetAll(users).Result.FirstOrDefault(x => x.UserName == users.UserName && x.PassWord == CommonBase.ToMD5(users.Password));
			if (user == null)
			{
				return null;
			}
			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
				 new Claim(AuthorSetting.UserName, user.UserName),
				 new Claim(AuthorSetting.ID, user.ID.ToString()),
			  }),
				Expires = DateTime.UtcNow.AddMinutes(double.Parse(_iconfiguration["JWT:Time"])),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
				
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}
		public async Task<IEnumerable<UserDTO>> GetAll(Users users)
		{
			var conn = _context.CreateConnection();
			using var rs = await conn.QueryMultipleAsync("GetAllUser", new { users .UserName}, commandType: CommandType.StoredProcedure);
			var result = await rs.ReadAsync<UserDTO>().ConfigureAwait(false);
			return result;
		}
		public Tokens GenerateToken(Users userName)
		{
			return GenerateJWTTokens(userName);
		}

		public Tokens GenerateRefreshToken(Users username)
		{
			return GenerateJWTTokens(username);
		}
		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}
		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var Key = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Key),
				ClockSkew = TimeSpan.Zero
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
			JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Invalid token");
			}
			return principal;
		}
	}
}
