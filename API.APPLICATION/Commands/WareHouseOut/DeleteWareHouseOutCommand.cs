using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.WareHouseOut
{
    public class DeleteWareHouseOutCommand : IRequest<MethodResult<DeleteWareHouseOutCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }

    public class DeleteWareHouseOutCommandResponse : DeleteWareHouseOutCommand
    {
    }
}