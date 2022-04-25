using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UserDTO
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HoDem { get; set; }
        public string Ten { get; set; }
    }
}
