

using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.Location.District
{
    public class DeleteDistrictCommand : IRequest<MethodResult<DeleteDistrictCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteDistrictCommandResponse { }
}
