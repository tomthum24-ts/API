namespace API.DOMAIN
{
    public class RolePermissions : APIEntity
    {
        #region Fields

        private string _nameController;
        private string _actionName;
        private string _note;
        private bool? _status;

        #endregion Fields

        #region Constructors

        private RolePermissions()
        {
        }

        public RolePermissions(string nameController, string actionName, string note, bool? status)
        {
            _nameController = nameController;
            _actionName = actionName;
            _note = note;
            _status = status;
        }

        #endregion Constructors

        #region Properties

        public string NameController { get => _nameController; }
        public string ActionName { get => _actionName; }
        public string Note { get => _note; }
        public bool? status { get => _status; }

        #endregion Properties

        #region Behaviours

        public void SetNameController(string nameController) => _nameController = nameController;

        public void SetActionName(string actionName) => _actionName = actionName;

        public void SetNote(string note) => _note = note;

        public void SetStatus(bool? status) => _status = status;

        #endregion Behaviours
    }
}