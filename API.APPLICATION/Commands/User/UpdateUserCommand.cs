using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.User
{
    public class UpdateUserCommand : IRequest<MethodResult<UpdateUserCommandResponse>>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
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
    public class UpdateUserCommandResponse : UpdateUserCommand
    {
    }
}
