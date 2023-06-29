
namespace API.DOMAIN
{
    public class Menu : APIEntity
    {
        #region Fields
        private string _name;
        private int? _parentID;
        private string _link;
        private string _image;
        private string _note;
        private bool? _status;

        #endregion Fields

        #region Constructors

        private Menu()
        {
        }

        public Menu(string name, int? parentID, string link, string image, string note,bool? status)
        {
            _name = name;
            _parentID = parentID;
            _link = link;
            _image = image;
            _note = note;
            _status = status;
        }
        #endregion Constructors

        #region Properties
        public string Name { get => _name; }
        public int? ParentID { get => _parentID; }
        public string Link { get => _link; }
        public string Image { get => _image; }
        public string Note { get => _note; }
        public bool? Status { get => _status; }

        #endregion Properties

        #region Behaviours
        public void SetName(string name) => _name = name;
        public void SetParentID(int? parentID) => _parentID = parentID;
        public void SetLink(string link) => _link = link;
        public void SetImage(string image) => _image = image;
        public void SetNote(string note) => _note = note;
        public void SetStatus(bool? status) => _status = status;

        #endregion Behaviours
    }
}
