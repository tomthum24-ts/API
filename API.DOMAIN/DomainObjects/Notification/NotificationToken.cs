namespace API.DOMAIN.DomainObjects.Notification
{
    public class NotificationToken : APIEntity
    {
        #region Fields

        private string _tokenFireBase;
        private string _userId;
        private string _userName;
        private string _deviceId;
        private string _note;

        #endregion Fields

        #region Constructors

        private NotificationToken()
        { 
        }

        public NotificationToken(string tokenFireBase, string userId, string userName, string deviceId, string note)
        {
            _tokenFireBase = tokenFireBase;
            _userId = userId;
            _userName = userName;
            _deviceId = deviceId;
            _note = note;
        }

        #endregion Constructors

        #region Properties

        public string TokenFireBase { get => _tokenFireBase; }
        public string UserId { get => _userId; }
        public string UserName { get => _userName; }
        public string DeviceId { get => _deviceId; }
        public string Note { get => _note; }

        #endregion Properties

        #region Behaviours

        public void SetTokenFireBase(string tokenFireBase) => _tokenFireBase = tokenFireBase;

        public void SetUserId(string userId) => _userId = userId;

        public void SetUserName(string userName) => _userName = userName;

        public void SetDeviceId(string deviceId) => _deviceId = deviceId;

        public void SetNote(string note) => _note = note;

        #endregion Behaviours
    }
}