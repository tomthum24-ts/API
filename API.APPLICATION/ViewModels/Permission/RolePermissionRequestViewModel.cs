using API.APPLICATION.ViewModels.BaseClasses;

namespace API.APPLICATION.ViewModels.Permission
{
    public class RolePermissionRequestViewModel : DanhMucRequestViewModel
    {

    }

    public class RolePermissionByIdRequestViewModel
    {
        public int Id { get; set; }
    }
}