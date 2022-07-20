
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace BaseCommon.Common.ClaimUser
{
    public class UserSessionInfo : IUserSessionInfo
    {
        private readonly Claim _userIdClaim;
        private readonly Claim _name;
        private readonly Claim _userName;
        public UserSessionInfo(IHttpContextAccessor httpContextAccessor)
        {
            //_userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst("ID");
            //_name = httpContextAccessor.HttpContext?.User.FindFirst(AuthorSetting.Name);
            //_userName = httpContextAccessor.HttpContext?.User.FindFirst(AuthorSetting.UserName);
        }

        public int? ID => !string.IsNullOrWhiteSpace(_userIdClaim?.Value) ? int.Parse(_userIdClaim?.Value) : null;

        public string Name => !string.IsNullOrWhiteSpace(_name?.Value) ? _name.Value : string.Empty;

        public string UserName => !string.IsNullOrWhiteSpace(_userName?.Value) ? _userName.Value : string.Empty;
    }
}
