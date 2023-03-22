using System;

namespace API.DOMAIN.DTOs.User
{
    public class UserDTO
    {
        public int ID { get; set; }
        public Guid GuiID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Ten { get; set; }
        public string DiaChi { get; set; }
        public string Phone { get; set; }
        public bool? TrangThai { get; set; }
    }
}
