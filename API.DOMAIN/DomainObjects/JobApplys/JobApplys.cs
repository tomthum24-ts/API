using Aspose.Words;
using AutoMapper.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DOMAIN
{
    public class JobApplys : APIEntity
    {
        #region Fields

        private string _name;
        private int? _idJobs;
        private int? _idUser;
        private string _timeApply;
        private int? _number;
        private string _note;

        #endregion Fields

        #region Constructors

        public JobApplys(string name,
                        int? idJobs,
                        int? idUser,
                        string timeApply,
                        int? number,
                        string note)
        {
            _name = name;
            _idJobs = idJobs;
            _idUser = idUser;
            _timeApply = timeApply;
            _number = number;
            _note = note;
        }

        private JobApplys()
        {

        }

        #endregion Constructors

        #region Properties
        public string Name { get => _name; }
        public int? IdJobs { get => _idJobs; }
        public int? IdUser { get => _idUser; }
        public string TimeApply { get => _timeApply; }
        public int? Number { get => _number; }
        public string Note { get => _note; }

        #endregion Properties

        #region Behaviours

        public void SetName(string name) => _name = name;
        public void SetIdJobs(int? idJobs) => _idJobs = idJobs;
        public void SetIdUser(int? idUser) => _idUser = idUser;
        public void SetTimeApply(string timeApply) => _timeApply = timeApply;
        public void SetNumber(int? number) => _number = number;
        public void SetNote(string note) => _note = note;

        #endregion Behaviours
    }
}
