using BaseCommon.Common.EnCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DOMAIN
{
    public class Customer : APIEntity
    {
        #region Fields

        private string _code;
        private string _name;
        private string _address;
        private int? _province;
        private int? _district;
        private int? _village;
        private string _phone;
        private string _phone2;
        private string _cMND;
        private DateTime? _birthday;
        private string _email;
        private string _note;
        private string _taxCode;
        private int? _groupMember;
        private int? _fileAttach;
        private bool? _isEnterprise;
        private string _enterpriseName;
        private string _representative;
        private string _poisition;
        private string _registrationNumber;
        private DateTime? _registrationDate;
        private string _registrationAddress;

        #endregion Fields

        #region Constructors

        public Customer(string code, string name, string address,int? province, int? district, int? village,
            string phone,string phone2, string cMND, DateTime? birthday,string email, string note, string taxCode,
            int? groupMember, int? fileAttach, bool? isEnterprise,string enterpriseName, string representative,string poisition, string registrationNumber,
            DateTime? registrationDate,string registrationAddress)
        {
            _code = code;
            _name = name;
            _address = address;
            _province = province;
            _district = district;
            _village = village;
            _phone = phone;
            _phone2 = phone2;
            _cMND = cMND;
            _birthday = birthday;
            _email = email;
            _note = note;
            _taxCode = taxCode;
            _groupMember = groupMember;
            _fileAttach = fileAttach;
            _isEnterprise = isEnterprise;
            _enterpriseName = enterpriseName;
            _representative = representative;
            _poisition = poisition;
            _registrationNumber = registrationNumber;
            _registrationDate = registrationDate;
            _registrationAddress = registrationAddress;
        }

        private Customer()
        {
           
        }

        #endregion Constructors

        #region Properties
        public string Code { get => _code; }
        public string Name { get => _name; }
        public string Address { get => _address; }
        public int? Province { get => _province; }
        public int? District { get => _district; }
        public int? Village { get => _village; }
        public string Phone { get => _phone; }
        public string Phone2 { get => _phone2; }
        public string CMND { get => _cMND; }
        public DateTime? Birthday { get => _birthday; }
        public string Email { get => _email; }
        public string Note { get => _note; }
        public string TaxCode { get => _taxCode; }
        public int? GroupMember { get => _groupMember; }
        public int? FileAttach { get => _fileAttach; }
        public bool? IsEnterprise { get => _isEnterprise; }
        public string EnterpriseName { get => _enterpriseName; }
        public string Representative { get => _representative; }
        public string Poisition { get => _poisition; }
        public string RegistrationNumber { get => _registrationNumber; }
        public DateTime? RegistrationDate { get => _registrationDate; }
        public string RegistrationAddress { get => _registrationAddress; }


        #endregion Properties

        #region Behaviours

        public void SetCode(string code) => _code = code;
        public void SetName(string name) => _name = name;
        public void SetAddress(string address) => _address = address;
        public void SetProvince(int? province) => _province = province;
        public void SetDistrict(int? district) => _district = district;
        public void SetVillage(int? village) => _village = village;
        public void SetPhone(string phone) => _phone = phone;
        public void SetPhone2(string phone2) => _phone2 = phone2;
        public void SetCMND(string cMND) => _cMND = cMND;
        public void SetBirthday(DateTime? birthday) => _birthday = birthday;
        public void SetEmail(string email) => _email = email;
        public void SetNote(string note) => _note = note;
        public void SetTaxCode(string taxCode) => _taxCode = taxCode;
        public void SetGroupMember(int? groupMember) => _groupMember = groupMember;
        public void SetFileAttach(int? fileAttach) => _fileAttach = fileAttach;
        public void SetIsEnterprise(bool? isEnterprise) => _isEnterprise = isEnterprise;
        public void SetEnterpriseName(string enterpriseName) => _enterpriseName = enterpriseName;
        public void SetRepresentative(string representative) => _representative = representative;
        public void SetPoisition(string poisition) => _poisition = poisition;
        public void SetRegistrationNumber(string registrationNumber) => _registrationNumber = registrationNumber;
        public void SetRegistrationDate(DateTime? registrationDate) => _registrationDate = registrationDate;
        public void SetRegistrationAddress(string registrationAddress) => _registrationAddress = registrationAddress;

        #endregion Behaviours
    }
}
