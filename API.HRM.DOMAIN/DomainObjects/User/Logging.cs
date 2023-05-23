using System;

namespace API.DOMAIN.DomainObjects.User
{
    public class Logging : APIEntity
    {
        #region Fields
        private string _tooken;
        private DateTime? _expired;
        private bool? _isActive;
        private string _devices;
        private string _ipAddress;

        #endregion Fields

        #region Constructors

        private Logging()
        {
        }

        public Logging(string tooken, DateTime? expired, bool? isActive,string devices, string ipAddress)
        {
            _tooken=tooken;
            _expired=expired;
            _isActive=isActive;
            _devices=devices;
            _ipAddress=ipAddress;
        }
        #endregion Constructors

        #region Properties
        public string Tooken { get => _tooken; }
        public DateTime? Expired { get => _expired; }
        public bool? IsActive { get => _isActive; }
        public string Devices { get => _devices; }
        public string IpAddress { get => _ipAddress; }


        #endregion Properties

        #region Behaviours
        public void SetTooken(string tooken) => _tooken = tooken;
        public void SetExpired(DateTime? expired) => _expired = expired;
        public void SetIsActive(bool? isActive) => _isActive = isActive;
        public void SetDevices(string devices) => _devices = devices;
        public void SetIpAddress(string ipAddress) => _ipAddress = ipAddress;

        #endregion Behaviours
    }

}

