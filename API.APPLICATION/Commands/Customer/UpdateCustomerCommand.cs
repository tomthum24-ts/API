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
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int? Province { get; set; } = 0;
        public int? District { get; set; } = 0;
        public int? Village { get; set; } = 0;
        public string Phone { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
        public string CMND { get; set; } = string.Empty;
        public DateTime? Birthday { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string TaxCode { get; set; } = string.Empty;
        public int? GroupMember { get; set; } = 0;
        public int? FileAttach { get; set; } = 0;
        public bool? IsEnterprise { get; set; } = false;
        public string EnterpriseName { get; set; } = string.Empty;
        public string Representative { get; set; } = string.Empty;
        public string Poisition { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public DateTime? RegistrationDate { get; set; } = DateTime.Now;
        public string RegistrationAddress { get; set; } = string.Empty;
    }
    public class UpdateCustomerCommandResponse : UpdateCustomerCommand
    {
    }
}
