namespace API.APPLICATION.ViewModels.Permission
{
    public class RolePermissionResponseViewModel
    {
        public int Id { get; set; }
        public string NameController { get; set; }
        public string ActionName { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
}