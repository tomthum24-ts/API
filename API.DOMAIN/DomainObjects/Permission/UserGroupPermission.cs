
namespace API.DOMAIN.DomainObjects.Permission
{
    public class UserGroupPermission
    {
        #region Fields
        private string _name;
        private string _note;

        #endregion Fields

        #region Constructors

        private UserGroupPermission()
        {
        }

        #endregion Constructors
        #region Properties

        public string Name { get => _name; }
        public string Note { get => _note; }

        #endregion Properties
        #region Behaviours
        public void SetName(string name) => _name = name;
        public void SetNote(string note) => _note = note;

        #endregion Behaviours
    }
}
