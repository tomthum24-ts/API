using API.APPLICATION.ViewModels.BaseClasses;

namespace API.APPLICATION.ViewModels.Vehicle
{
    public class VehicleRequestViewModel : DanhMucRequestViewModel
    {
    }

    public class VehicleByIdRequestViewModel
    {
        public int Id { get; set; }
    }
}