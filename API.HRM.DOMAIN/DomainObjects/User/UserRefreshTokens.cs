using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.HRM.DOMAIN.DomainObjects.User
{
    public class UserRefreshTokens
    {
        #region Fields
        [Key]
        private int _id;
        private string _userName;
        private string _refreshToken;
        private bool? _isActive;
        private DateTime? _createdDate;
        private string _createdBy;
        private DateTime? _modifiedDate;
        private string _modifiedBy;

        #endregion Fields

        #region Constructors

        private UserRefreshTokens()
        {
        }

        public UserRefreshTokens(string userName, string refreshToken, bool? isActive)
        {
            _userName = userName;
            _refreshToken = refreshToken;
            _isActive = isActive;
            

        }
        #endregion Constructors

        #region Properties
        public int Id { get => _id; }
        public string UserName { get => _userName; }
        public string RefreshToken { get => _refreshToken; }
        public bool? IsActive { get => _isActive; }
        public DateTime? CreatedDate { get => _createdDate; }
        public string CreatedBy { get => _createdBy; }
        public DateTime? ModifiedDate { get => _modifiedDate; }
        public string ModifiedBy { get => _modifiedBy; }

        #endregion Properties

        #region Behaviours
        public void SetId(int id) => _id = id;
        public void SetUserName(string userName) => _userName = userName;
        public void SetRefreshToken(string refreshToken) => _refreshToken = refreshToken;
        public void SetIsActive(bool? isActive) => _isActive = isActive;
        public void SetCreatedDate(DateTime? createdDate) => _createdDate = createdDate;
        public void SetCreatedBy(string createdBy) => _createdBy = createdBy;
        public void SetModifiedDate(DateTime? modifiedDate) => _modifiedDate = modifiedDate;
        public void SetModifiedBy(string modifiedBy) => _modifiedBy = modifiedBy;

        #endregion Behaviours
    }
}
