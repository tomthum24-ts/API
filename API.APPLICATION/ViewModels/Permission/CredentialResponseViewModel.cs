namespace API.APPLICATION.ViewModels.Permission
{
    public class CredentialResponseViewModel
    {
        public int Id { get; set; }
        public int? UserGroupId { get; set; }
        public int? RoleId { get; set; }
        public bool? Status { get; set; }
    }
}