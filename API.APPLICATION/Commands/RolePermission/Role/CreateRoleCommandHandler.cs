using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission.Role
{
    public class CreateRoleCommandHandler : IRequest<MethodResult<CreateRoleCommandHandlerResponse>>
    {
    }
    public class CreateRoleCommandHandlerResponse : CreateRoleCommandHandler
    {

    }
}
