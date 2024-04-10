using API.APPLICATION.Commands.WareHouseIn;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseOut
{
    public class ChangeStatusWareHouseOutCommand : IRequest<MethodResult<ChangeStatusWareHouseOutCommandResponse>>
    {
        public int Id { get; set; }
        public int Status { get; set; }

    }
    public class ChangeStatusWareHouseOutCommandResponse : ChangeStatusWareHouseOutCommand
    {

    }
}
