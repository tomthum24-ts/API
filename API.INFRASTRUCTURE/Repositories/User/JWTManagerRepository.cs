using API.HRM.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Services.User;
using BaseCommon.Common.EnCrypt;
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

		private readonly IConfiguration iconfiguration;

		public readonly DapperContext _context;
		public JWTManagerRepository(IConfiguration iconfiguration, DapperContext context)
		{
			this.iconfiguration = iconfiguration;
			_context = context;
		}
		public Tokens GenerateJWTTokens(Users users)
		{
			var user = GetAll().Result;
			if (!user.Any(x => x.UserName == users.Name && x.PassWord == CommonBase.ToMD5(users.Password)))
			{
				return null;
			}

			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Name, users.Name)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(double.Parse(iconfiguration["JWT:Time"])),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),

			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}
		public async Task<IEnumerable<UserDTO>> GetAll()
		{
			var conn = _context.CreateConnection();
			using var rs = await conn.QueryMultipleAsync("GetAllUser", new { }, commandType: CommandType.StoredProcedure);
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
			var Key = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);

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
