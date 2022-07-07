using BaseCommon.Common.EnCrypt;
using System.ComponentModel.DataAnnotations;

namespace API.HRM.DOMAIN
{
    public class User : APIEntity
    {
        #region Fields
       
     
        private string _userName;
        private string _password;
        private string _name;
        private string _address;
        private string _phone;
        private bool? _status;


        #endregion Fields

        #region Constructors

        private User()
        {
        }

        public User(string userName, string name, string address, string phone, bool? status)
        {
            _userName = userName;
            _name = name;
            _password = CommonBase.ToMD5(userName);
            _address = address;
            _phone = phone;
            _status = status;

        }
        #endregion Constructors

        #region Properties
     
        public string UserName { get => _userName; }
        public string PassWord { get => _password; }
        public string Name { get => _name; }
        public string Address { get => _address; }
        public string Phone { get => _phone; }
        public bool? status { get => _status; }

        #endregion Properties

        #region Behaviours
    
        public void SetUserName(string userName) { _userName = userName; }
        public void SetPassWord(string passWord) { _password = passWord; }
        public void SetName(string name) => _name = name;
        public void SetAddress(string address) => _address = address;
        public void SetPhone(string phone) => _phone = phone;
        public void SetStatus(bool? status) => _status = status;

        #endregion Behaviours
    }
}
