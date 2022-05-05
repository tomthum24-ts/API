
using System;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string GroupID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? ProvinceID { get; set; }
        public int? DistrictID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool Status { get; set; }
    }
}
