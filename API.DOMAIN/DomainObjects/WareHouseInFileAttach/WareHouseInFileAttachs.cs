namespace API.DOMAIN.DomainObjects.WareHouseInFileAttach
{
    public class WareHouseInFileAttachs : APIEntity
    {
        #region Fields

        private int _idWareHouseIn;
        private string _name;
        private string _path;

        #endregion Fields

        #region Constructors

        public WareHouseInFileAttachs(int idWareHouseIn, string name, string path)
        {
            _idWareHouseIn = idWareHouseIn;
            _name = name;
            _path = path;
        }

        #endregion Constructors

        #region Properties

        public int IdWareHouseIn { get => _idWareHouseIn; }
        public string Name { get => _name; }
        public string Path { get => _path; }

        #endregion Properties

        #region Behaviours

        public void SetIdWareHouseIn(int idWareHouseIn) => _idWareHouseIn = idWareHouseIn;

        public void SetName(string name) => _name = name;

        public void SetPath(string path) => _path = path;

        #endregion Behaviours
    }
}