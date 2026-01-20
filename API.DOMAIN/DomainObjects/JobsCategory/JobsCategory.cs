using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DOMAIN
{
    [Table(TableConstants.JOBSCATEGORY_TABLENAME)]
    public class JobsCategory : APIEntity
    {
        #region Fields
        private string _code;
        private string _name;
        private string _note;
        #endregion

        #region Constructors
        public JobsCategory() { }

        public JobsCategory(string code, string name, string note)
        {
            _code = code;
            _name = name;
            _note = note;
        }
        #endregion

        #region Properties
        public string Code { get => _code; }
        public string Name { get => _name; }
        public string Note { get => _note; }
        #endregion
        
        #region Behaviours
        public void SetCode(string code) => _code = code;
        public void SetName(string name) => _name = name;
        public void SetNote(string note) => _note = note;
        #endregion
    }
}
