namespace API.DOMAIN.DomainObjects.WareHouseOutDetail
{
    public class WareHouseOutDetail : APIEntity
    {
        #region Fields

        private int? _idWareHouseOut;
        private int? _rangeOfVehicle;
        private decimal? _quantityVehicle;
        private int? _productId;
        private decimal? _quantityProduct;
        private int? _unit;
        private string _size;
        private decimal? _weight;
        private string _guildId;
        private string _rONumber;
        private string _lotNo;
        private string _totalWeighScan;
        private string _productDate;
        private string _expiryDate;
        private string _note;
        private string _madeIn;
        private string _productName;
        private string _unitName;
        private string _containerNumber;
        private string _vehicleNumber;
        #endregion Fields

        #region Constructors

        private WareHouseOutDetail()
        {
        }
        public WareHouseOutDetail(int? idWareHouseOut, int? rangeOfVehicle, decimal? quantityVehicle, int? productId, decimal? quantityProduct,
            int? unit, string size, decimal? weight, string rONumber, string guildId, string note, string lotNo,string totalWeighScan,
            string productDate,string expiryDate,string madeIn,string productName,string unitName,string containerNumber,string vehicleNumber)
        {
            _idWareHouseOut = idWareHouseOut;
            _rangeOfVehicle = rangeOfVehicle;
            _quantityVehicle = quantityVehicle;
            _productId = productId;
            _quantityProduct = quantityProduct;
            _unit = unit;
            _size = size;
            _weight = weight;
            _rONumber = rONumber;
            _guildId = guildId;
            _note = note;
            _lotNo = lotNo;
            _totalWeighScan = totalWeighScan;
            _productDate = productDate;
            _expiryDate = expiryDate;
            _madeIn = madeIn;
            _productName = productName;
            _unitName = unitName;
            _containerNumber = containerNumber;
            _vehicleNumber = vehicleNumber;
        }
        #endregion Constructors

        #region Properties

        public int? IdWareHouseOut { get => _idWareHouseOut; }
        public int? RangeOfVehicle { get => _rangeOfVehicle; }
        public decimal? QuantityVehicle { get => _quantityVehicle; }
        public int? ProductId { get => _productId; }
        public decimal? QuantityProduct { get => _quantityProduct; }
        public int? Unit { get => _unit; }
        public string Size { get => _size; }
        public decimal? Weight { get => _weight; }

        public string RONumber { get => _rONumber; }
        public string LotNo { get; set; }
        public string TotalWeighScan { get; set; }
        public string ProductDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Note { get; set; }
        public string MadeIn { get; set; }
        public string GuildId { get;set; }
        public string ProductName { get => _productName; }
        public string UnitName { get => _unitName; }
        public string ContainerNumber { get; set; }
        public string VehicleNumber { get; set; }

        #endregion Properties

        #region Behaviours

        public void SetIdWareHouseOut(int? idWareHouseOut) => _idWareHouseOut = idWareHouseOut;

        public void SetRangeOfVehicle(int? rangeOfVehicle) => _rangeOfVehicle = rangeOfVehicle;

        public void SetQuantityVehicle(decimal? quantityVehicle) => _quantityVehicle = quantityVehicle;

        public void SetProductId(int? productId) => _productId = productId;

        public void SetQuantityProduct(decimal? quantityProduct) => _quantityProduct = quantityProduct;
        public void SetUnit(int? unit) => _unit = unit;
        public void SetSize(string size) => _size = size;
        public void SetWeight(decimal? weight) => _weight = weight;
        public void SetRONumber(string rONumber) => _rONumber = rONumber;
        public void SetLotNo(string lotNo) => _lotNo = lotNo;
        public void SetTotalWeighScan(string totalWeighScan) => _totalWeighScan = totalWeighScan;
        public void SetProductDate(string productDate) => _productDate = productDate;
        public void SetExpiryDate(string expiryDate) => _expiryDate = expiryDate;
        public void SetNote(string note) => _note = note;
        public void SetMadeIn(string madeIn) => _madeIn = madeIn;
        public void SetGuildId(string guildId) => _guildId = guildId;
        public void SetProductName(string productName) => _productName = productName;
        public void SetUnitName(string unitName) => _unitName = unitName; 
        public void SetContainerNumber(string containerNumber) => _containerNumber = containerNumber; 
        public void SetVehicleNumber(string vehicleNumber) => _vehicleNumber = vehicleNumber;
        #endregion Behaviours
    }
}