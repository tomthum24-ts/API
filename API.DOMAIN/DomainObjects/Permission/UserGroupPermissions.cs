
namespace API.DOMAIN.DomainObjects.Permission
{
    public class UserGroupPermissions : APIEntity
    {
        #region Fields
        private string _name;
        private string _note;
        private bool? _status;
        #endregion Fields

        #region Constructors

        public UserGroupPermissions()
        {
        }
        public UserGroupPermissions( string name, string note, bool? status)
        {
            _name = name;
            _note = note;
            _status = status;
        }
        #endregion Constructors
        #region Properties

        public string Name { get => _name; }
        public string Note { get => _note; }
        public bool ? Status { get => _status; }

        #endregion Properties
        #region Behaviours
        public void SetName(string name) => _name = name;
        public void SetNote(string note) => _note = note;
        public void SetStatus(bool? status) => _status = status;
        #endregion Behaviours
    }
}
