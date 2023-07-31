using API.APPLICATION.ViewModels.BaseClasses;

namespace API.APPLICATION.ViewModels.Permission
{
    public class UserGroupPermissionRequestViewModel : DanhMucRequestViewModel
    {
    }

    public class UserGroupPermissionByIdRequestViewModel
    {
        public int Id { get; set; }
    }
}