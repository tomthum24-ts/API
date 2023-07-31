using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.RolePermission.Role
{
    public class DeleteRoleCommand : IRequest<MethodResult<DeleteRoleCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }

    public class DeleteRoleCommandResponse : DeleteRoleCommand
    {
        public DeleteRoleCommandResponse(List<DeleteRoleCommand> datas)
        {
            Datas = datas;
        }

        public List<DeleteRoleCommand> Datas { get; }
    }
}