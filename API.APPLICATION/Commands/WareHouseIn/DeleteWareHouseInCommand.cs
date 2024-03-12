using API.APPLICATION.Commands.Customer;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseIn
{
    public class DeleteWareHouseInCommand : IRequest<MethodResult<DeleteWareHouseInCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteWareHouseInCommandResponse : DeleteWareHouseInCommand
    {

    }
}
