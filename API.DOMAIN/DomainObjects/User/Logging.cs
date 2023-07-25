using System;

namespace API.DOMAIN.DomainObjects.User
{
    public class Logging : APIEntity
    {
        #region Fields
        private string _token;
        private DateTime? _expired;
        private bool? _isActive;
        private string _devices;
        private string _ipAddress;
        private string _brownserName;
        private string _brownserVersion;
        private string _deviceHash;
        private string _engineName;
        private string _engineVersion;
        private string _osName;
        private string _osVersion;
        private string _timeZone;
        private string _type;
        private string _userAgent;

        #endregion Fields

        #region Constructors

        private Logging()
        {
        }

        public Logging(string token, DateTime? expired, bool? isActive,string devices, string ipAddress)
        {
            _token=token;
            _expired=expired;
            _isActive=isActive;
            _devices=devices;
            _ipAddress=ipAddress;
        }
        #endregion Constructors

        #region Properties
        public string Token { get => _token; }
        public DateTime? Expired { get => _expired; }
        public bool? IsActive { get => _isActive; }
        public string Devices { get => _devices; }
        public string IpAddress { get => _ipAddress; }
        public string BrownserName { get => _brownserName; }
        public string BrownserVersion { get => _brownserVersion; }
        public string DeviceHash { get => _deviceHash; }
        public string EngineName { get => _engineName; }
        public string EngineVersion { get => _engineVersion; }
        public string OsName { get => _osName; }
        public string OsVersion { get => _osVersion; }
        public string TimeZone { get => _timeZone; }
        public string Type { get => _type; }
        public string UserAgent { get => _userAgent; }


        #endregion Properties

        #region Behaviours
        public void SetToken(string token) => _token = token;
        public void SetExpired(DateTime? expired) => _expired = expired;
        public void SetIsActive(bool? isActive) => _isActive = isActive;
        public void SetDevices(string devices) => _devices = devices;
        public void SetIpAddress(string ipAddress) => _ipAddress = ipAddress;
        public void SetBrownserName(string brownserName) => _brownserName = brownserName;
        public void SetBrownserVersion(string brownserVersion) => _brownserVersion = brownserVersion;
        public void SetDeviceHash(string deviceHash) => _deviceHash = deviceHash;
        public void SetEngineName(string engineName) => _engineName = engineName;
        public void SetEngineVersion(string engineVersion) => _engineVersion = engineVersion;
        public void SetOsName(string osName) => _osName = osName;
        public void SetOsVersion(string osVersion) => _osVersion = osVersion;
        public void SetTimeZone(string timeZone) => _timeZone = timeZone;
        public void SetType(string type) => _type = type;
        public void SetUserAgent(string userAgent) => _userAgent = userAgent;

        #endregion Behaviours
    }

}

