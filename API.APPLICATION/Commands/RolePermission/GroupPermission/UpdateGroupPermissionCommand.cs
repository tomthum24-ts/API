using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.GroupPermission
{
    public class UpdateGroupPermissionCommand : IRequest<MethodResult<UpdateGroupPermissionCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class UpdateGroupPermissionCommandResponse : UpdateGroupPermissionCommand
    {

    }
}
