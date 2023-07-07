using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.GroupPermission
{
    public class CreateGroupPermissionCommand : IRequest<MethodResult<CreateGroupPermissionCommandResponse>>
    {
    }
    public class CreateGroupPermissionCommandResponse : CreateGroupPermissionCommand
    {

    }
}
