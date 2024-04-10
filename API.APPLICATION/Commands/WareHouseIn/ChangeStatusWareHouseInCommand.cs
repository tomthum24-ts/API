using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseIn
{
    public class ChangeStatusWareHouseInCommand : IRequest<MethodResult<ChangeStatusWareHouseInCommandResponse>>
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
    public class ChangeStatusWareHouseInCommandResponse : ChangeStatusWareHouseInCommand
    {

    }
}
