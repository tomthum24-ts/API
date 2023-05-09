using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.Location.Village
{
    public class DeleteVillageCommand : IRequest<MethodResult<DeleteVillageCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteVillageCommandResponse { }
}
