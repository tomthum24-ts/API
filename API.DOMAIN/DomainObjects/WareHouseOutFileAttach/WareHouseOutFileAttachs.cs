using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DOMAIN.DomainObjects.WareHouseOutFileAttach
{
    public class WareHouseOutFileAttachs : APIEntity
    {
        #region Fields
        private int _idWareHouseOut;
        private string _name;
        private string _path;

        #endregion Fields

        #region Constructors
        public WareHouseOutFileAttachs(int idWareHouseOut, string name, string path)
        {
            _idWareHouseOut = idWareHouseOut;
            _name = name;
            _path = path;
        }


        #endregion Constructors

        #region Properties
        public int IdWareHouseOut { get => _idWareHouseOut; }
        public string Name { get => _name; }
        public string Path { get => _path; }
        #endregion Properties

        #region Behaviours
        public void SetIdWareHouseOut(int idWareHouseOut) => _idWareHouseOut = idWareHouseOut;
        public void SetName(string name) => _name = name;
        public void SetPath(string path) => _path = path;

        #endregion Behaviours
    }
}
