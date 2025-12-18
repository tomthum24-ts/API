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
    public class DeleteJobsCommand : IRequest<MethodResult<DeleteJobsCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteJobsCommandResponse
    {

    }
}
