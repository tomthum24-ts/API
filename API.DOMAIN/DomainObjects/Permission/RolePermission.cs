using System;

namespace API.DOMAIN.DomainObjects.Permission
{
    public class RolePermission : APIEntity
    {
        #region Fields
        private string _nameControl;
        private string _name;

        #endregion Fields

        #region Constructors

        private RolePermission()
        {
        }

        #endregion Constructors
        #region Properties

        public string NameControl { get => _nameControl; }
        public string Name { get => _name; }

        #endregion Properties
        #region Behaviours
        public void SetNameControl(string nameControl) => _nameControl = nameControl;
        public void SetName(string name) => _name = name;

        #endregion Behaviours
    }
}
