using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Login
{
    public class LoginSSOCommand : IRequest<MethodResult<LoginCommandResponse>>
    {
        public int Type { get; set; } //1. google 2. FB 3.Apple
        public string AccessToken { get; set; }
    }
}
