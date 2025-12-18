using API.APPLICATION.Commands.Customer;
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

namespace API.APPLICATION.Commands.JobApplys
{
    internal class DeleteJobApplysCommandHandler : IRequestHandler<DeleteJobApplysCommand, MethodResult<DeleteJobApplysCommandResponse>>
    {
        private readonly IJobApplysRepository _jobApplysRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobApplysCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IJobApplysRepository jobApplysRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jobApplysRepository = jobApplysRepository;
        }

        public async Task<MethodResult<DeleteJobApplysCommandResponse>> Handle(DeleteJobApplysCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteJobApplysCommandResponse>();
            var existingUser = await _jobApplysRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingUser == null || existingUser.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _jobApplysRepository.DeleteRange(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            return methodResult;
        }
    }
}
