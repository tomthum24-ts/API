using API.Common;
using MediatR;

namespace API.Commands
{
    public class UpdateUserCommand : IRequest<MethodResult<UpdateUserCommandResponse>>
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Status
        {
            get; set;
        }
    }
    public class UpdateUserCommandResponse : UpdateUserCommand
    {
    }
}
