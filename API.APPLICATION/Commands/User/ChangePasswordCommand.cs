using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.User
{
    public class ChangePasswordCommand : IRequest<MethodResult<ChangePasswordCommandResponse>>
    {
        public int id { get; set; }
        public string Password { get; set; }
    }
    public class ChangePasswordCommandResponse : ChangePasswordCommand
    {
    }
}
