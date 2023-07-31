using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.GroupPermission
{
    public class DeleteGroupPermissionCommand : IRequest<MethodResult<DeleteGroupPermissionCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }

    public class DeleteGroupPermissionCommandResponse : DeleteGroupPermissionCommand
    {
        public DeleteGroupPermissionCommandResponse(List<DeleteGroupPermissionCommand> datas)
        {
            Datas = datas;
        }

        public List<DeleteGroupPermissionCommand> Datas { get; }
    }
}