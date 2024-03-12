using API.APPLICATION.Commands.Customer;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseIn
{
    public class DeleteWareHouseInCommandHandler : IRequestHandler<DeleteWareHouseInCommand, MethodResult<DeleteWareHouseInCommandResponse>>
    {
        public Task<MethodResult<DeleteWareHouseInCommandResponse>> Handle(DeleteWareHouseInCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
