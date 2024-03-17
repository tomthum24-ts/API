using API.APPLICATION.ViewModels.BaseClasses;

namespace API.APPLICATION.ViewModels.Unit
{
    public class UnitRequestViewModel : DanhMucRequestViewModel
    {
    }

    public class UnitByIdRequestViewModel
    {
        public int Id { get; set; }
    }
}