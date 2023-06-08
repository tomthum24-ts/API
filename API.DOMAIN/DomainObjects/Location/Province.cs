
namespace API.DOMAIN
{
    public class Province : APIEntity
    {
        #region Fields
        private string _provinceCode;
        private string _provinceName;
        private string _codeName;
        private string _divisionType;
        private string _note;
        private bool? _status;
        #endregion
        #region Constructors
        public Province()
        {
        }
        public Province(string provinceCode, string provinceName, string codeName,string divisionType,string note,bool? status)
        {

            _provinceCode=provinceCode;
            _provinceName=provinceName;
            _codeName=codeName;
            _divisionType=divisionType;
            _note = note;
            _status = status;
        }

        #endregion
        #region Properties
        public string ProvinceCode { get => _provinceCode; }
        public string ProvinceName { get => _provinceName; }
        public string CodeName { get => _codeName; }
        public string DivisionType { get => _divisionType; }
        public string Note { get => _note; }
        public bool? Status { get => _status; }
        #endregion
        #region Behaviours
        public void SetProvinceCode(string provinceCode) => _provinceCode = provinceCode;
        public void SetProvinceName(string provinceName) => _provinceName = provinceName;
        public void SetCodeName(string codeName) => _codeName = codeName;
        public void SetDivisionType(string divisionType) => _divisionType = divisionType;
        public void SetNote(string note) => _note = note;
        public void SetStatus(bool? status) => _status = status;
        #endregion
    }
}
