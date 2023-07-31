using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission.Role
{
    public class UpdateRoleCommand : IRequest<MethodResult<UpdateRoleCommandResponse>>
    {
        public int Id { get; set; }
        public string NameController { get; set; }
        public string ActionName { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateRoleCommandResponse : UpdateRoleCommand
    {

    }
}
