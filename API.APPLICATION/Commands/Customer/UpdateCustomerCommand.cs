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
    public class UpdateCustomerCommand : IRequest<MethodResult<UpdateCustomerCommandResponse>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public int? Village { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string CMND { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string TaxCode { get; set; }
        public int? GroupMember { get; set; }
        public int? FileAttach { get; set; }
        public bool? IsEnterprise { get; set; }
        public string EnterpriseName { get; set; }
        public string Representative { get; set; }
        public string Poisition { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string RegistrationAddress { get; set; }
    }
    public class UpdateCustomerCommandResponse : UpdateCustomerCommand
    {
    }
}
