using API.DOMAIN.DTOs.Permission;
using API.DOMAIN.DTOs.User;
using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.Enum;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
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
using System.Threading;
using System.Threading.Tasks;

namespace API.INFRASTRUCTURE.Repositories.User
{
	public class JWTManagerRepository : IJWTManagerRepository
	{

		private readonly IConfiguration _iconfiguration;
		private readonly IUserRepository _userRepository;
		private readonly DapperContext _context;
		//private readonly IDistributedCached<UserItemCache> _distributedCache;
		private readonly IMemoryCache _memoryCache;
		public JWTManagerRepository(IConfiguration iconfiguration, DapperContext context, IUserRepository userRepository, IMemoryCache memoryCache)
        {
            _iconfiguration = iconfiguration;
            _context = context;
            _userRepository = userRepository;
			_memoryCache = memoryCache;
        }
        public async Task<Tokens> GenerateJWTTokens(Users users, CancellationToken cancellationToken)
		{
			
			
			var user= await _userRepository.Get(x => x.UserName == users.UserName && x.PassWord == users.Password).FirstOrDefaultAsync(cancellationToken);
			var groupId = new PermissionParam();
			groupId.IdGroup = user.UserGroup  ;

			var data = await GetAllPermissionNotPaging(groupId);
			var lstPermission = data.Select(x => x.RoleController);
			var cacheOptions = new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMinutes(int.Parse(_iconfiguration["JWT:TimeSliding"])))
				.SetAbsoluteExpiration(TimeSpan.FromMinutes(int.Parse(_iconfiguration["JWT:TimeAbsolute"])));
			_memoryCache.Set(AuthorSetting.NamePermission, lstPermission, cacheOptions);
			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
			var time = int.Parse(_iconfiguration["JWT:Time"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
				 new Claim(AuthorSetting.UserName, users.UserName),
				 new Claim(AuthorSetting.ID, user.Id.ToString()),
				 new Claim(AuthorSetting.LastName,user.LastName.ToString()),
				 new Claim(AuthorSetting.Email,user.Email.ToString()),
				 new Claim(AuthorSetting.Project,user.Project.ToString()),
				 new Claim(AuthorSetting.Permissiongroups,user.UserGroup.ToString())
			  }),
				Expires = DateTime.UtcNow.AddMinutes(time),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
				
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			return new Tokens { Token = tokenHandler.WriteToken(token), ExpiresIn= token.ValidTo };

		}
		//public async Task<IEnumerable<UserDTO>> GetAll(Users users)
		//{
		//	var conn = _context.CreateConnection();
		//	using var rs = await conn.QueryMultipleAsync("GetAllUser", new { users .UserName}, commandType: CommandType.StoredProcedure);
		//	var result = await rs.ReadAsync<UserDTO>().ConfigureAwait(false);
		//	return result;
		//}
		public async Task<Tokens> GenerateToken(Users userName, CancellationToken cancellationToken)
		{
			return await  GenerateJWTTokens(userName, cancellationToken);
		}

		public async Task<Tokens> GenerateRefreshToken(Users username, CancellationToken cancellationToken)
		{
			return await GenerateJWTTokens(username, cancellationToken);
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
		public async Task<IEnumerable<PermissionDTO>> GetAllPermissionNotPaging(PermissionParam param)
		{
			var conn = _context.CreateConnection();
			using var rs = await conn.QueryMultipleAsync("SP_PM_GetListPermission", param, commandType: CommandType.StoredProcedure);
			var result = await rs.ReadAsync<PermissionDTO>().ConfigureAwait(false);
			return result;
		}
	}
}
