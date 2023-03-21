using BaseCommon.Common.Enum;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace BaseCommon.Common.ClaimUser
{
    public class UserSessionInfo : IUserSessionInfo
    {
        private readonly Claim _userIdClaim;
        private readonly Claim _name;
        private readonly Claim _userName;
        private readonly Claim _lastName;
        private readonly Claim _email;
        private readonly Claim _project;
            

        public UserSessionInfo(IHttpContextAccessor httpContextAccessor)
        {
            _userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(AuthorSetting.ID);
            _name = httpContextAccessor.HttpContext?.User.FindFirst(AuthorSetting.Name);
            _userName = httpContextAccessor.HttpContext?.User.FindFirst(AuthorSetting.UserName);
            _lastName = httpContextAccessor.HttpContext?.User.FindFirst(AuthorSetting.LastName);
            _email = httpContextAccessor.HttpContext?.User.FindFirst(AuthorSetting.Email);
            _project = httpContextAccessor.HttpContext?.User.FindFirst(AuthorSetting.Project);
        }

        public int? ID => !string.IsNullOrWhiteSpace(_userIdClaim?.Value) ? int.Parse(_userIdClaim?.Value) : null;

        public string Name => !string.IsNullOrWhiteSpace(_name?.Value) ? _name.Value : string.Empty;
        public string UserName => !string.IsNullOrWhiteSpace(_userName?.Value) ? _userName.Value : string.Empty;
        public string LastName => !string.IsNullOrWhiteSpace(_lastName?.Value) ? _lastName.Value : string.Empty;
        public string Email => !string.IsNullOrWhiteSpace(_email?.Value) ? _email.Value : string.Empty;
        public int? Project => !string.IsNullOrWhiteSpace(_project?.Value) ? int.Parse(_project?.Value) : null;

    }
}
