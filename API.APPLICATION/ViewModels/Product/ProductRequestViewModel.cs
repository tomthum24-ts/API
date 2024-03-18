using API.APPLICATION.ViewModels.BaseClasses;

namespace API.APPLICATION.ViewModels.Product
{
    public class ProductRequestViewModel : DanhMucRequestViewModel
    {
    }

    public class ProductByIdRequestViewModel
    {
        public int Id { get; set; }
    }
}