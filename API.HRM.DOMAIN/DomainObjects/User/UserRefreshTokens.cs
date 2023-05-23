using System;

namespace API.DOMAIN
{
    public class RefreshToken : APIEntity
    {
        #region Fields
        private string _token;
        private string _idRefreshToken;
        private DateTime? _expires;
        private string _ipAddress;

        #endregion Fields

        #region Constructors

        private RefreshToken()
        {
        }

        public RefreshToken(string token,string idrefreshTooken, DateTime? expires, string ipAddress)
        {
            _token = token;
            _idRefreshToken = idrefreshTooken;
            _expires = expires;
            _ipAddress = ipAddress;
        }
        #endregion Constructors

        #region Properties
        public string Token { get => _token; }
        public string IdRefreshToken { get => _idRefreshToken; }
        public DateTime? Expires { get => _expires; }
        public string IpAddress { get => _ipAddress; }

        #endregion Properties

        #region Behaviours
        public void SetToken(string token) => _token = token;
        public void SetRefreshTooken(string refreshToken) => _idRefreshToken = refreshToken;
        public void SetExpires(DateTime expires) => _expires = expires;
        public void SetIpAddress(string ipAddress) => _ipAddress = ipAddress;

        #endregion Behaviours
    }
}
