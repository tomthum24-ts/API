
namespace API.DOMAIN
{
    public class Project : APIEntity
    {
        #region Fields
        private string _projectCode;
        private string _projectName;
        private string _note;
        private bool? _status;

        #endregion Fields

        #region Constructors

        public Project()
        {
        }

        public Project(string projectCode, string projectName, string note, bool? status)
        {
            _projectCode = projectCode;
            _projectName = projectName;
            _note = note;
            _status = status;

        }
        #endregion Constructors

        #region Properties
        public string ProjectCode { get => _projectCode; }
        public string ProjectName { get => _projectName; }
        public string Note { get => _note; }
        public bool? Status { get => _status; }

        #endregion Properties

        #region Behaviours
        public void SetProjectCode(string projectCode) => _projectCode = projectCode;
        public void SetProjectName(string projectName) => _projectName = projectName;
        public void SetNote(string note) => _note = note;
        public void SetStatus(bool? status) => _status = status;

        #endregion Behaviours
    }
}
