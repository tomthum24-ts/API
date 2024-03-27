namespace API.DOMAIN.DomainObjects.WareHouseInDetail
{
    public class WareHouseInDetail : APIEntity
    {
        #region Fields
        private int? _idWareHouseIn;
        private int? _rangeOfVehicle;
        private decimal? _quantityVehicle;
        private int? _productId;
        private decimal? _quantityProduct;
        private int? _unit;
        private decimal? _size;
        private decimal? _weight;
        private string _guildId;
        private string _lotNo;
        private string _totalWeighScan;
        private string _productDate;
        private string _expiryDate;
        private string _note;
        private string _madeIn;

        #endregion Fields

        #region Constructors
        public WareHouseInDetail()
        {

        }

        public WareHouseInDetail(int? idWareHouseIn,int? rangeOfVehicle, decimal? quantityVehicle, int? productId,
            decimal? quantityProduct, int? unit, decimal? size, decimal? weight, string guildId, string note, string lotNo, string totalWeighScan, string productDate, string expiryDate, string madeIn)
        {
            _idWareHouseIn = idWareHouseIn;
            _rangeOfVehicle = rangeOfVehicle;
            _quantityVehicle = quantityVehicle;
            _productId = productId;
            _quantityProduct = quantityProduct;
            _unit = unit;
            _size = size;
            _weight = weight;
            _guildId = guildId;
            _note = note;
            _lotNo = lotNo;
            _totalWeighScan = totalWeighScan;
            _productDate = productDate;
            _expiryDate = expiryDate;
            _madeIn = madeIn;
        }

        #endregion Constructors

        #region Properties

        public int? IdWareHouseIn { get => _idWareHouseIn; }
        public int? RangeOfVehicle { get => _rangeOfVehicle; }
        public decimal? QuantityVehicle { get => _quantityVehicle; }
        public int? ProductId { get => _productId; }
        public decimal? QuantityProduct { get => _quantityProduct; }
        public int? Unit { get => _unit; }
        public decimal? Size { get => _size; }
        public decimal? Weight { get => _weight; }
        public string GuildId { get; set; }
        public string LotNo { get; set; }
        public string TotalWeighScan { get; set; }
        public string ProductDate { get; set; }
        public string ExpiryDate { get; set; }
        public string Note { get; set; }
        public string MadeIn { get; set; }
        #endregion Properties

        #region Behaviours

        public void SetIdWareHouseIn(int? idWareHouseIn) => _idWareHouseIn = idWareHouseIn;
        public void SetRangeOfVehicle(int? rangeOfVehicle) => _rangeOfVehicle = rangeOfVehicle;
        public void SetQuantityVehicle(decimal? quantityVehicle) => _quantityVehicle = quantityVehicle;
        public void SetProductId(int? productId) => _productId = productId;
        public void SetQuantityProduct(decimal? quantityProduct) => _quantityProduct = quantityProduct;
        public void SetUnit(int? unit) => _unit = unit;
        public void SetSize(decimal? size) => _size = size;
        public void SetWeight(decimal? weight) => _weight = weight;
        public void SetGuildId(string guildId) => _guildId = guildId;
        public void SetLotNo(string lotNo) => _lotNo = lotNo;
        public void SetTotalWeighScan(string totalWeighScan) => _totalWeighScan = totalWeighScan;
        public void SetProductDate(string productDate) => _productDate = productDate;
        public void SetExpiryDate(string expiryDate) => _expiryDate = expiryDate;
        public void SetNote(string note) => _note = note;
        public void SetMadeIn(string madeIn) => _madeIn = madeIn;
        #endregion Behaviours
    }
}