namespace API.DOMAIN.DTOs.Permission
{
    public class UserGroupPermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
}