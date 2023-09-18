using System;

namespace API.DOMAIN
{
    public class UserRefreshToken : APIEntity
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
        private DateTime? _timeLogout;
        private string _userAgent;
        private string _type;
        private string _oSName;
        private string _oSVersion;
        private string _deviceHash;
        private string _brownName;
        private string _brownVersion;
        private string _timeZone;
        private bool? _isLogout;
        #endregion Fields

        #region Constructors

        private UserRefreshToken()
        {
        }

        public UserRefreshToken(string idrefreshToken, DateTime? expires, string ipAddress,string userLogin,DateTime? revoked,
            string revokedByIp, bool? isRevoked, bool? isActive, string userAgent, string type, string osName, string osVerrsion,
            string deviceHash, string brownName, string brownVersion, string timeZone
)
        {

            _idRefreshToken = idrefreshToken;
            _expires = expires;
            _ipAddress = ipAddress;
            _userLogin=userLogin;
            _revoked = revoked;
            _revokedByIp=revokedByIp;
            _isRevoked=isRevoked;
            _isActive=isActive;
            _userAgent=userAgent;
            _type=type;
            _oSName=osName;
            _oSVersion=osVerrsion;
            _deviceHash=deviceHash;
            _brownName=brownName;
            _brownVersion=brownVersion;
            _timeZone=timeZone;
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
        public DateTime? TimeLogout { get => _revoked; }
        public string UserAgent { get => _userAgent; }
        public string Type { get => _type; }
        public string OSName { get => _oSName; }
        public string OSVersion { get => _oSVersion; }
        public string DeviceHash { get => _deviceHash; }
        public string BrownName { get => _brownName; }
        public string BrownVersion { get => _brownVersion; }
        public string TimeZone { get => _timeZone; }
        public bool? IsLogout { get => _isLogout; }
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
        public void SetTimeLogout(DateTime? timeLogout) => _timeLogout = timeLogout;
        public void SetUserAgent(string userAgent) => _userAgent = userAgent;
        public void SetType(string type) => _type = type;
        public void SetOSName(string oSName) => _oSName = oSName;
        public void SetOSVersion(string oSVersion) => _oSVersion = oSVersion;
        public void SetDeviceHash(string deviceHash) => _deviceHash = deviceHash;
        public void SetBrownName(string brownName) => _brownName = brownName;
        public void SetBrownVersion(string brownVersion) => _brownVersion = brownVersion;
        public void SetTimeZone(string timeZone) => _timeZone = timeZone;
        public void SetIsLogout(bool? isLogout) => _isLogout = isLogout;
        #endregion Behaviours
    }
}
