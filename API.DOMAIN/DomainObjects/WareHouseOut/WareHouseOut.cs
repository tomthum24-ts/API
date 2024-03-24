using System;

namespace API.DOMAIN.DomainObjects.WareHouseOut
{
    public class WareHouseOut : APIEntity
    {
        #region Fields

        private string _code;
        private DateTime? _dateCode;
        private int? _customerID;
        private string _representative;
        private DateTime? _intendTime;
        private int? _wareHouse;
        private string _customerName;
        private string _filePath;
        private string _fileName;
        private string _seal;
        private string _temp;
        private string _carNumber;
        private string _container;
        private string _door;
        private string _deliver;
        private string _veterinary;
        private decimal? _cont;
        private string _note;
        private string _ortherNote;
        private int? _fileAttach;
        private string _numberCode;
        private string _invoiceNumber;
        private DateTime? _timeStart;
        private DateTime? _timeEnd;

        #endregion Fields

        #region Constructors

        public WareHouseOut(string code, DateTime? dateCode, int? customerID, string representative, DateTime? intendTime, int? wareHouse, string customerName,
            string filePath, string fileName, string seal, string temp, string carNumber, string container, string door, string deliver, string veterinary,
            decimal? cont, string note, string ortherNote, int? fileAttach,
            string numberCode, string invoiceNumber, DateTime? timeStart, DateTime? timeEnd)
        {
            _code = code;
            _dateCode = dateCode;
            _customerID = customerID;
            _representative = representative;
            _intendTime = intendTime;
            _wareHouse = wareHouse;
            _customerName = customerName;
            _filePath = filePath;
            _fileName = fileName;
            _seal = seal;
            _temp = temp;
            _carNumber = carNumber;
            _container = container;
            _door = door;
            _deliver = deliver;
            _veterinary = veterinary;
            _cont = cont;
            _note = note;
            _ortherNote = ortherNote;
            _fileAttach = fileAttach;
            _numberCode = numberCode;
            _invoiceNumber = invoiceNumber;
            _timeStart = timeStart;
            _timeEnd = timeEnd;
        }

        private WareHouseOut()
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
        public string CustomerName { get => _customerName; }
        public string FilePath { get => _filePath; }
        public string FileName { get => _fileName; }
        public string Seal { get => _seal; }
        public string Temp { get => _temp; }
        public string CarNumber { get => _carNumber; }
        public string Container { get => _container; }
        public string Door { get => _door; }
        public string Deliver { get => _deliver; }
        public string Veterinary { get => _veterinary; }
        public decimal? Cont { get => _cont; }
        public string NumberCode { get => _numberCode; }
        public string InvoiceNumber { get => _invoiceNumber; }
        public DateTime? TimeStart { get => _timeStart; }
        public DateTime? TimeEnd { get => _timeEnd; }

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

        public void SetCustomerName(string customerName) => _customerName = customerName;

        public void SetFilePath(string filePath) => _filePath = filePath;

        public void SetFileName(string fileName) => _fileName = fileName;

        public void SetSeal(string seal) => _seal = seal;

        public void SetTemp(string temp) => _temp = temp;

        public void SetCarNumber(string carNumber) => _carNumber = carNumber;

        public void SetContainer(string container) => _container = container;

        public void SetDoor(string door) => _door = door;

        public void SetDeliver(string deliver) => _deliver = deliver;

        public void SetVeterinary(string veterinary) => _veterinary = veterinary;

        public void SetCont(decimal? cont) => _cont = cont;

        public void SetNumberCode(string numberCode) => _numberCode = numberCode;

        public void SetInvoiceNumber(string invoiceNumber) => _invoiceNumber = invoiceNumber;

        public void SetTimeStart(DateTime? timeStart) => _timeStart = timeStart;

        public void SetTimeEnd(DateTime? timeEnd) => _timeEnd = timeEnd;

        #endregion Behaviours
    }
}