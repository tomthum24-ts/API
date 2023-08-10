
namespace API.DOMAIN
{
    public class PM_Credential : APIEntity
    {
        #region Fields
        private int _userGroupId;
        private int _roleId;
        private string _note;
        private bool? _status;

        #endregion Fields

        #region Constructors

        private PM_Credential()
        {
        }
        public PM_Credential(int userGroupId, int roleId)
        {
            _userGroupId = userGroupId;
            _roleId = roleId;

        }

        #endregion Constructors
        #region Properties

        public int UserGroupId { get => _userGroupId; }
        public int RoleId { get => _roleId; }
        public string Note { get => _note; }

        public bool? Status { get => _status; }
        #endregion Properties
        #region Behaviours
        public void SetUserGroupId(int userGroupId) => _userGroupId = userGroupId;
        public void SetRoleId(int roleId) => _roleId = roleId;
        public void SetNote(string note) => _note = note;
        public void SetStatus(bool? status) => _status = status;
        #endregion Behaviours
    }
}
