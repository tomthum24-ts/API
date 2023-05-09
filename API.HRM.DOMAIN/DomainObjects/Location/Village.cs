

namespace API.DOMAIN
{
    public class Village : APIEntity
    {
        #region Fields
        private string _villageCode;
        private string _villageName;
        private string _codeName;
        private string _divisionType;
        private int? _idDistrict;
        private string _note;
        private bool? _status;

        #endregion Fields

        #region Constructors
        private Village()
        {
        }

        public Village(string villageCode, string villageName, string codeName, string divisionType, int? idDistrict, string note, bool? status)
        {
            _villageCode = villageCode;
            _villageName = villageName;
            _codeName = codeName;
            _divisionType = divisionType;
            _idDistrict = idDistrict;
            _note = note;
            _status = status;
        }
        #endregion Constructors

        #region Properties
        public string VillageCode { get => _villageCode; }
        public string VillageName { get => _villageName; }
        public string CodeName { get => _codeName; }
        public string DivisionType { get => _divisionType; }
        public int? IdDistrict { get => _idDistrict; }
        public string Note { get => _note; }
        public bool? Status { get => _status; }


        #endregion Properties

        #region Behaviours
        public void SetVillageCode(string villageCode) => _villageCode = villageCode;
        public void SetVillageName(string villageName) => _villageName = villageName;
        public void SetCodeName(string codeName) => _codeName = codeName;
        public void SetDivisionType(string divisionType) => _divisionType = divisionType;
        public void SetIdDistrict(int? idDistrict) => _idDistrict = idDistrict;
        public void SetNote(string note) => _note = note;
        public void SetStatus(bool? status) => _status = status;

        #endregion Behaviours
    }
}
