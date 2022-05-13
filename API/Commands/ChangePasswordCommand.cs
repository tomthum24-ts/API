using API.Common;
using MediatR;

namespace API.Commands
{
    public class ChangePasswordCommand :IRequest<MethodResult<ChangePasswordCommandResponse>>
    {
        public int id { get; set; }
        public string Password { get; set; }
    }
    public class ChangePasswordCommandResponse : ChangePasswordCommand
    {
    }
}
