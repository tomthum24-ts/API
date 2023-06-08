
namespace API.DOMAIN
{
    public class District : APIEntity
    {
        #region Fields
        private string _districtCode;
        private string _districtName;
        private string _codeName;
        private string _divisionType;
        private int? _idProvince;
        private string _note;
        private bool? _status;

        #endregion Fields

        #region Constructors
        public District()
        {
        }

        public District(string districtCode,string districtName,string codeName,string divisionType,int? idProvince,string note,bool? status)
        {
            _districtCode = districtCode;
            _districtName=districtName;
            _codeName=codeName;
            _divisionType=divisionType;
            _idProvince=idProvince;
            _note=note;
            _status=status;
        }
        #endregion Constructors

        #region Properties

        public string DistrictCode { get => _districtCode; }
        public string DistrictName { get => _districtName; }
        public string CodeName { get => _codeName; }
        public string DivisionType { get => _divisionType; }
        public int? IdProvince { get => _idProvince; }
        public string Note { get => _note; }
        public bool? Status { get => _status; }

        #endregion Properties

        #region Behaviours
        public void SetDistrictCode(string districtCode) => _districtCode = districtCode;
        public void SetDistrictName(string districtName) => _districtName = districtName;
        public void SetCodeName(string codeName) => _codeName = codeName;
        public void SetDivisionType(string divisionType) => _divisionType = divisionType;
        public void SetIdProvince(int? idProvince) => _idProvince = idProvince;
        public void SetNote(string note) => _note = note;
        public void SetStatus(bool? status) => _status = status;

        #endregion Behaviours
    }
}
