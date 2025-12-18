using API.APPLICATION.Commands.JobApplys;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Jobs
{
    public class UpdateJobsCommand : IRequest<MethodResult<UpdateJobsCommandResponse>>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Category { get; set; }
        public DateTime? TimeStart { get; set; }
        public int? TimeZone { get; set; }
        public int? JobNumber { get; set; }
        public int JobNumberRemain { get; set; }
        public bool? IsHome { get; set; }
        public bool? IsEating { get; set; }
        public int? Province { get; set; }
        public int? District { get; set; }
        public int? Village { get; set; }
        public string Address { get; set; }
        public int? TypeJob { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public int? TypePayment { get; set; }
        public int? TimePayment { get; set; }
        public string Detail { get; set; }
        public string Require { get; set; }
        public int? Cancel { get; set; }
        public int? Tools { get; set; }
    }
    public class UpdateJobsCommandResponse
    {

    }
}
