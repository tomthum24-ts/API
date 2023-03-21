using BaseCommon.Common.EnCrypt;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.HRM.DOMAIN
{
    public class User : APIEntity
    {
        #region Fields
        private string _userName;
        private string _passWord;
        private string _name;
        private string _lastName;
        private string _email;
        private string _address;
        private string _phone;
        private int? _department;
        private DateTime? _birthDay;
        private int? _province;
        private int? _district;
        private int? _village;
        private int? _project;
        private string _note;
        private DateTime? _creationDate;
        private int? _createdById;
        private DateTime? _updateDate;
        private int? _updatedById;
        private DateTime? _deletionDate;
        private int? _deletedById;
        private bool? _isDelete;
        private bool? _status;

        #endregion Fields

        #region Constructors

        private User()
        {
        }

        public User(string userName, string name,string lastName, string email, string address, string phone, int? department,
            DateTime? birthDay, int? province,int? district, int? village,int? project, string note, bool? status)
        {


            _userName = userName;
            _passWord = CommonBase.ToMD5(userName);
            _name = name;
            _lastName = lastName;
            _email = email;
            _address = address;
            _phone = phone;
            _department = department;
            _birthDay = birthDay;
            _province = province;
            _district = district;
            _village = village;
            _project = project;
            _note = note;
            _status = status;

        }
        #endregion Constructors

        #region Properties
        public string UserName { get => _userName; }
        public string PassWord { get => _passWord; }
        public string Name { get => _name; }
        public string LastName { get => _lastName; }
        public string Email { get => _email; }
        public string Address { get => _address; }
        public string Phone { get => _phone; }
        public int? Department { get => _department; }
        public DateTime? BirthDay { get => _birthDay; }
        public int? Province { get => _province; }
        public int? District { get => _district; }
        public int? Village { get => _village; }
        public int? Project { get => _project; }
        public string Note { get => _note; }
        public DateTime? CreationDate { get => _creationDate; }
        public int? CreatedById { get => _createdById; }
        public DateTime? UpdateDate { get => _updateDate; }
        public int? UpdatedById { get => _updatedById; }
        public DateTime? DeletionDate { get => _deletionDate; }
        public int? DeletedById { get => _deletedById; }
        public bool? IsDelete { get => _isDelete; }
        public bool? Status { get => _status; }

        #endregion Properties

        #region Behaviours
        public void SetUserName(string userName) => _userName = userName;
        public void SetPassWord(string passWord) => _passWord = passWord;
        public void SetName(string name) => _name = name;
        public void SetLastName(string lastName) => _lastName = lastName;
        public void SetEmail(string email) => _email = email;
        public void SetAddress(string address) => _address = address;
        public void SetPhone(string phone) => _phone = phone;
        public void SetDepartment(int? department) => _department = department;
        public void SetBirthDay(DateTime? birthday) => _birthDay = birthday;
        public void SetProvince(int? province) => _province = province;
        public void SetDistrict(int? district) => _district = district;
        public void SetVillage(int? village) => _village = village;
        public void SetProject(int? project) => _project = project;
        public void SetNote(string note) => _note = note;
        public void SetStatus(bool? status) => _status = status;

        #endregion Behaviours
    }
}
