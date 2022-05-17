using BaseCommon.Common.MethodResult;
using MediatR;
using System.Text.Json.Serialization;

namespace API.APPLICATION.Commands.User
{
    public class CreateUserCommand : IRequest<MethodResult<CreateUserCommandResponse>>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Status
        {
            get; set;
        }
    }
    public class CreateUserCommandResponse : CreateUserCommand
    {

    }
}
