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


        #endregion Properties

        #region Behaviours
        public void SetToken(string token) => _token = token;
        public void SetExpired(DateTime? expired) => _expired = expired;
        public void SetIsActive(bool? isActive) => _isActive = isActive;
        public void SetDevices(string devices) => _devices = devices;
        public void SetIpAddress(string ipAddress) => _ipAddress = ipAddress;

        #endregion Behaviours
    }

}

