using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Commands.JobApplys;
using API.APPLICATION.Commands.Jobs;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using Aspose.Words;
using AutoMapper;
using AutoMapper.Execution;
using BaseCommon.Common.MethodResult;
using BaseCommon.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    internal class CreateJobApplysCommandHandler : IRequestHandler<CreateJobApplysCommand, MethodResult<CreateJobApplysCommandResponse>>
    {
        private readonly IJobApplysRepository _jobApplysRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobApplysCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IJobApplysRepository jobApplysRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jobApplysRepository = jobApplysRepository;
        }

        public async Task<MethodResult<CreateJobApplysCommandResponse>> Handle(CreateJobApplysCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateJobApplysCommandResponse>();
            var createJobApplys = new JobApplys(
                request.Name,
                request.IdJobs,
                request.IdUser,
                request.TimeApply,
                request.Number,
                request.Note
                );
            _jobApplysRepository.Add(createJobApplys);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateJobApplysCommandResponse>(createJobApplys);
            return methodResult;
        }
    }
}
