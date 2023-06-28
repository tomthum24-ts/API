using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DOMAIN.DTOs.Permission
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public int? UserGroupId { get; set; }
        public int? RoleId { get; set; }
        public bool? Status { get; set; }
        public string Note { get; set; }
        public string NameController { get; set; }
        public string ActionName { get; set; }
        public string Name { get; set; }
        public string RoleController { get; set; }
    }
}
