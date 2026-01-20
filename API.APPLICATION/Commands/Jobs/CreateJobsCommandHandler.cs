using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Commands.JobApplys;
using API.APPLICATION.Commands.Jobs;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using Aspose.Words;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Newtonsoft.Json;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateJobsCommandHandler : IRequestHandler<CreateJobsCommand, MethodResult<CreateJobsCommandResponse>>
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobsCommandHandler(IJobsRepository jobsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _jobsRepository = jobsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MethodResult<CreateJobsCommandResponse>> Handle(CreateJobsCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateJobsCommandResponse>();
            var createJobs = new Jobs(
                request.Code,
                request.Name,
                request.Category,
                request.TimeStart,
                request.TimeZone,
                request.JobNumber,
                request.JobNumberRemain,
                request.IsHome,
                request.IsEating,
                request.Province,
                request.Village,
                request.Address,
                request.TypeJob,
                request.PriceFrom,
                request.PriceTo,
                request.TypePayment,
                request.TimePayment,
                request.Detail,
                request.Require,
                request.Cancel,
                request.Tools
                );
            _jobsRepository.Add(createJobs);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateJobsCommandResponse>(createJobs);
            return methodResult;
        }
    }
}
