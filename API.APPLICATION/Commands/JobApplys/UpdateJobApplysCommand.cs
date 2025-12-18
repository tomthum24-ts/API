using API.APPLICATION.Commands.Customer;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.JobApplys
{
    public class UpdateJobApplysCommand : IRequest<MethodResult<UpdateJobApplysCommandResponse>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? IdJobs { get; set; }
        public int? IdUser { get; set; }
        public string TimeApply { get; set; }
        public int? Number { get; set; }
        public string Note { get; set; }
    }
    public class UpdateJobApplysCommandResponse
    {

    }
}
