using System;

namespace API.DOMAIN
{
    public class RefreshToken : APIEntity
    {
        #region Fields
        private string _idRefreshToken;
        private DateTime? _expires;
        private string _ipAddress;
        private string _userLogin;
        private DateTime? _revoked;
        private string _revokedByIp;
        private string _replacedByToken;
        private string _reasonRevoked;
        private bool? _isRevoked;
        private bool? _isActive;

        #endregion Fields

        #region Constructors

        private RefreshToken()
        {
        }

        public RefreshToken(string idrefreshToken, DateTime? expires, string ipAddress,string userLogin,DateTime? revoked,string revokedByIp, bool? isRevoked, bool? isActive)
        {

            _idRefreshToken = idrefreshToken;
            _expires = expires;
            _ipAddress = ipAddress;
            _userLogin=userLogin;
            _revoked = revoked;
            _revokedByIp=revokedByIp;
            _isRevoked=isRevoked;
            _isActive=isActive;
        }
        #endregion Constructors

        #region Properties
        public string IdRefreshToken { get => _idRefreshToken; }
        public DateTime? Expires { get => _expires; }
        public string IpAddress { get => _ipAddress; }
        public string UserLogin { get => _userLogin; }
        public DateTime? Revoked { get => _revoked; }
        public string RevokedByIp { get => _revokedByIp; }
        public string ReplacedByToken { get => _replacedByToken; }
        public string ReasonRevoked { get => _reasonRevoked; }
        public bool? IsRevoked { get => _isRevoked; }
        public bool? IsActive { get => _isActive; }

        #endregion Properties

        #region Behaviours
        public void SetIdRefreshToken(string idRefreshToken) => _idRefreshToken = idRefreshToken;
        public void SetExpires(DateTime? expires) => _expires = expires;
        public void SetIpAddress(string ipAddress) => _ipAddress = ipAddress;
        public void SetUserLogin(string userLogin) => _userLogin = userLogin;
        public void SetRevoked(DateTime? revoked) => _revoked = revoked;
        public void SetRevokedByIp(string revokedByIp) => _revokedByIp = revokedByIp;
        public void SetReplacedByToken(string replacedByToken) => _replacedByToken = replacedByToken;
        public void SetReasonRevoked(string reasonRevoked) => _reasonRevoked = reasonRevoked;
        public void SetIsRevoked(bool? isRevoked) => _isRevoked = isRevoked;
        public void SetIsActive(bool? isActive) => _isActive = isActive;

        #endregion Behaviours
    }
}
