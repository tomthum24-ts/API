using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.GroupPermission
{
    public class DeleteGroupPermissionCommand : IRequest<MethodResult<DeleteGroupPermissionCommandResponse>>
    {
    }
    public class DeleteGroupPermissionCommandResponse : DeleteGroupPermissionCommand
    {

    }
}
