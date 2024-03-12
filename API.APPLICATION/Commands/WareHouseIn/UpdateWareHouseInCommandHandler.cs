using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseIn
{
    public class UpdateWareHouseInCommandHandler : IRequestHandler<UpdateWareHouseInCommand, MethodResult<UpdateWareHouseInCommandResponse>>
    {
        public Task<MethodResult<UpdateWareHouseInCommandResponse>> Handle(UpdateWareHouseInCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}