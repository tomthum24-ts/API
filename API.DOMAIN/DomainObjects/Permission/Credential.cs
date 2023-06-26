
namespace API.DOMAIN.DomainObjects.Permission
{
    public class Credential : APIEntity
    {
        #region Fields
        private string _userGroupId;
        private string _roleId;

        #endregion Fields

        #region Constructors

        private Credential()
        {
        }

        #endregion Constructors
        #region Properties

        public string UserGroupId { get => _userGroupId; }
        public string RoleId { get => _roleId; }

        #endregion Properties
        #region Behaviours
        public void SetUserGroupId(string userGroupId) => _userGroupId = userGroupId;
        public void SetRoleId(string roleId) => _roleId = roleId;

        #endregion Behaviours
    }
}
