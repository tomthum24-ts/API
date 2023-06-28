using System;

namespace API.DOMAIN.DomainObjects.Permission
{
    public class RolePermission : APIEntity
    {
        #region Fields
        private string _nameController;
        private string _actionName;
        private string _note;

        #endregion Fields

        #region Constructors

        private RolePermission()
        {
        }

        #endregion Constructors
        #region Properties

        public string NameController { get => _nameController; }
        public string ActionName { get => _actionName; }
        public string Note { get => _note; }

        #endregion Properties
        #region Behaviours
        public void SetNameController(string nameController) => _nameController = nameController;
        public void SetActionName(string actionName) => _actionName = actionName;
        public void SetNote(string note) => _note = note;

        #endregion Behaviours
    }
}
