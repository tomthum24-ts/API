using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Commands.Jobs;
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
    internal class UpdateJobApplysCommandHandler : IRequestHandler<UpdateJobApplysCommand, MethodResult<UpdateJobApplysCommandResponse>>
    {
        private readonly IJobApplysRepository _jobApplysRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobApplysCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IJobApplysRepository jobApplysRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jobApplysRepository = jobApplysRepository;
        }

        public async Task<MethodResult<UpdateJobApplysCommandResponse>> Handle(UpdateJobApplysCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateJobApplysCommandResponse>();
            var isExistData = await _jobApplysRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }
            isExistData.SetName(request.Name);
            isExistData.SetIdJobs(request.IdJobs);
            isExistData.SetIdUser(request.IdUser);
            isExistData.SetTimeApply(request.TimeApply);
            isExistData.SetNumber(request.Number);
            isExistData.SetNote(request.Note);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateJobApplysCommandResponse>(isExistData);
            return methodResult;
        }
    }
}
