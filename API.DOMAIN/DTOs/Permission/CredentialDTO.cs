namespace API.DOMAIN.DTOs.Permission
{
    public class CredentialDTO
    {
        public int Id { get; set; }
        public int UserGroupId { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; }
    }
}