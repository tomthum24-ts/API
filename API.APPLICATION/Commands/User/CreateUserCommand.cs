using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace API.APPLICATION.Commands.User
{
    public class CreateUserCommand : IRequest<MethodResult<CreateUserCommandResponse>>
    {
        public string UserName { get; set; }
        [JsonIgnore]
        public string PassWord { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? Department { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public int? Village { get; set; }
        public int? Project { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class CreateUserCommandResponse : CreateUserCommand
    {

    }
}
