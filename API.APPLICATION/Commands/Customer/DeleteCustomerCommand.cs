using API.APPLICATION.Commands.User;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Customer
{
    public class DeleteCustomerCommand : IRequest<MethodResult<DeleteCustomerCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteCustomerCommandResponse { }
}
