using API.APPLICATION.ViewModels.BaseClasses;

namespace API.APPLICATION.ViewModels.CustomerGroup
{
    public class CustomerGroupRequestViewModel : DanhMucRequestViewModel
    {
    }

    public class CustomerGroupByIdRequestViewModel
    {
        public int Id { get; set; }
    }
}