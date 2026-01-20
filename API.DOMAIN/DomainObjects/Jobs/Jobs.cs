using Newtonsoft.Json;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace API.DOMAIN
{
    public class Jobs : APIEntity
    {
        #region Fields

        private string _code;
        private string _name;
        private int? _category;
        private DateTime? _timeStart;
        private int? _timeZone;
        private int? _jobNumber;
        private int? _jobNumberRemain;
        private bool? _isHome;
        private bool? _isEating;
        private int? _province;
        private int? _district;
        private int? _village;
        private string _address;
        private int? _typeJob;
        private decimal? _priceFrom;
        private decimal? _priceTo;
        private int? _typePayment;
        private int? _timePayment;
        private string _detail;
        private string _require;
        private int? _cancel;
        private int? _tools;

        #endregion Fields

        #region Constructors

        public Jobs(string code,
                    string name,
                    int? category,
                    DateTime? timeStart,
                    int? timeZone,
                    int? jobNumber,
                    int? jobNumberRemain,
                    bool? isHome,
                    bool? isEating,
                    int? province,
                    int? village,
                    string address,
                    int? typeJob,
                    decimal? priceFrom,
                    decimal? priceTo,
                    int? typePayment,
                    int? timePayment,
                    string detail,
                    string require,
                    int? cancel,
                    int? tools)
        {
            _code = code;
            _name = name;
            _category = category;
            _timeStart = timeStart;
            _timeZone = timeZone;
            _jobNumber = jobNumber;
            _jobNumberRemain = jobNumberRemain;
            _isHome = isHome;
            _isEating = isEating;
            _province = province;
            _village = village;
            _address = address;
            _typeJob = typeJob;
            _priceFrom = priceFrom;
            _priceTo = priceTo;
            _typePayment = typePayment;
            _timePayment = timePayment;
            _detail = detail;
            _require = require;
            _cancel = cancel;
            _tools = tools;
        }

        private Jobs()
        {

        }

        #endregion Constructors

        #region Properties
        public string Code { get => _code; }
        public string Name { get => _name; }
        public int? Category { get => _category; }
        public DateTime? TimeStart { get => _timeStart; }
        public int? TimeZone { get => _timeZone; }
        public int? JobNumber { get => _jobNumber; }
        public int? JobNumberRemain { get => _jobNumberRemain; }
        public bool? IsHome { get => _isHome; }
        public bool? IsEating { get => _isEating; }
        public int? Province { get => _province; }
        public int? District { get => _district; }
        public int? Village { get => _village; }
        public string Address { get => _address; }
        public int? TypeJob { get => _typeJob; }
        public decimal? PriceFrom { get => _priceFrom; }
        public decimal? PriceTo { get => _priceTo; }
        public int? TypePayment { get => _typePayment; }
        public int? TimePayment { get => _timePayment; }
        public string Detail { get => _detail; }
        public string Require { get => _require; }
        public int? Cancel { get => _cancel; }
        public int? Tools { get => _tools; }


        #endregion Properties

        #region Behaviours

        public void SetCode(string code) => _code = code;
        public void SetName(string name) => _name = name;
        public void SetCategory(int? category) => _category = category;
        public void SetTimeStart(DateTime? timeStart) => _timeStart = timeStart;
        public void SetTimeZone(int? timeZone) => _timeZone = timeZone;
        public void SetJobNumber(int? jobNumber) => _jobNumber = jobNumber;
        public void SetJobNumberRemain(int? jobNumberRemain) => _jobNumberRemain = jobNumberRemain;
        public void SetIsHome(bool? isHome) => _isHome = isHome;
        public void SetIsEating(bool? isEating) => _isEating = isEating;
        public void SetProvince(int? province) => _province = province;
        public void SetDistrict(int? district) => _district = district;
        public void SetVillage(int? village) => _village = village;
        public void SetAddress(string address) => _address = address;
        public void SetTypeJob(int? typeJob) => _typeJob = typeJob;
        public void SetPriceFrom(decimal? priceFrom) => _priceFrom = priceFrom;
        public void SetPriceTo(decimal? priceTo) => _priceTo = priceTo;
        public void SetTypePayment(int? typePayment) => _typePayment = typePayment;
        public void SetTimePayment(int? timePayment) => _timePayment = timePayment;
        public void SetDetail(string detail) => _detail = detail;
        public void SetRequire(string require) => _require = require;
        public void SetCancel(int? cancel) => _cancel = cancel;
        public void SetTools(int? tools) => _tools = tools;

        #endregion Behaviours
    }
}
