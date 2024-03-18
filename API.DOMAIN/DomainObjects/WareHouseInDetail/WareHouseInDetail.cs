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
        

        #endregion Fields

        #region Constructors
        public WareHouseInDetail()
        {

        }

        public WareHouseInDetail(int? idWareHouseIn,int? rangeOfVehicle, decimal? quantityVehicle, int? productId, decimal? quantityProduct, int? unit, decimal? size, decimal? weight)
        {
            _idWareHouseIn = idWareHouseIn;
            _rangeOfVehicle = rangeOfVehicle;
            _quantityVehicle = quantityVehicle;
            _productId = productId;
            _quantityProduct = quantityProduct;
            _unit = unit;
            _size = size;
            _weight = weight;
          
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
        

        #endregion Behaviours
    }
}