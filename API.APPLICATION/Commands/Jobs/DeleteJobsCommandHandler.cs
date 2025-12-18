using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Commands.JobApplys;
using API.Extension;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Jobs
{
    internal class DeleteJobsCommandHandler : IRequestHandler<DeleteJobsCommand, MethodResult<DeleteJobsCommandResponse>>
    {
        private readonly IJobsRepository _jobsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobsCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IJobsRepository jobsRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jobsRepository = jobsRepository;
        }

        public async Task<MethodResult<DeleteJobsCommandResponse>> Handle(DeleteJobsCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteJobsCommandResponse>();
            var existingUser = await _jobsRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingUser == null || existingUser.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _jobsRepository.DeleteRange(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            return methodResult;
        }
    }
}
