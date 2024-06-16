using System;

namespace API.DOMAIN.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int? STT { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Note { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string VillageName { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
