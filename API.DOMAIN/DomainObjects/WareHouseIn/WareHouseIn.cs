using Aspose.Words;
using System;

namespace API.DOMAIN
{
    public class WareHouseIn : APIEntity
    {
        #region Fields

        private string _code;
        private DateTime? _dateCode;
        private int? _customerID;
        private string _representative;
        private DateTime? _intendTime;
        private int? _wareHouse;
        private string _note;
        private string _ortherNote;
        private int? _fileAttach;

        #endregion Fields

        #region Constructors
        public WareHouseIn( string code, DateTime? dateCode, int? customerID, string representative, DateTime? intendTime,int? wareHouse,string note,string ortherNote,int? fileAttach)
        {
            _code = code;
            _dateCode = dateCode;
            _customerID = customerID;
            _representative = representative;
            _intendTime = intendTime;
            _wareHouse = wareHouse;
            _note = note;
            _ortherNote = ortherNote;
            _fileAttach = fileAttach;
        }
        private WareHouseIn()
        {
        }

        #endregion Constructors

        #region Properties

        public string Code { get => _code; }
        public DateTime? DateCode { get => _dateCode; }
        public int? CustomerID { get => _customerID; }
        public string Representative { get => _representative; }
        public DateTime? IntendTime { get => _intendTime; }
        public int? WareHouse { get => _wareHouse; }
        public string Note { get => _note; }
        public string OrtherNote { get => _ortherNote; }
        public int? FileAttach { get => _fileAttach; }

        #endregion Properties

        #region Behaviours

        public void SetCode(string code) => _code = code;

        public void SetDateCode(DateTime? dateCode) => _dateCode = dateCode;

        public void SetCustomerID(int? customerID) => _customerID = customerID;

        public void SetRepresentative(string representative) => _representative = representative;

        public void SetIntendTime(DateTime? intendTime) => _intendTime = intendTime;

        public void SetWareHouse(int? wareHouse) => _wareHouse = wareHouse;

        public void SetNote(string note) => _note = note;

        public void SetOrtherNote(string ortherNote) => _ortherNote = ortherNote;

        public void SetFileAttach(int? fileAttach) => _fileAttach = fileAttach;

        #endregion Behaviours
    }
}