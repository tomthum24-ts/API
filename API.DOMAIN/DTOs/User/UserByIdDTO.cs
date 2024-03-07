using System;

namespace API.DOMAIN.DTOs.User
{
    public class UserByIdDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? Department { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? Province { get; set; }
        public string ProvinceName { get; set; }
        public int? District { get; set; }
        public string DistrictName { get; set; }
        public int? Village { get; set; }
        public string VillageName { get; set; }
        public int? Project { get; set; }
        public string Note { get; set; }
    }
}