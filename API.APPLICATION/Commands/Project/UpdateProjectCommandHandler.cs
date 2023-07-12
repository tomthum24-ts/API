using API.DOMAIN;
using API.INFRASTRUCTURE.Interface;
using BaseCommon.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Project
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, MethodResult<UpdateProjectCommandResponse>>
    {
        private readonly IProjectRepository _ProjectRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProjectCommandHandler(IMapper mapper, IProjectRepository projectRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _ProjectRepository = projectRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<MethodResult<UpdateProjectCommandResponse>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateProjectCommandResponse>();
            var isExistData = await _ProjectRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }
            var existingProject = await _ProjectRepository.Get(x => x.ProjectCode == request.ProjectCode && x.Id != request.Id).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (existingProject == null)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.ProjectCode), request.ProjectCode)
                    });
                return methodResult;
            }

            isExistData.SetProjectCode(request.ProjectCode);
            isExistData.SetProjectName(request.ProjectName);
            isExistData.SetNote(request.Note);;
            isExistData.SetStatus(request.Status);
            _ProjectRepository.Update(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateProjectCommandResponse>(isExistData);
            return methodResult;
        }
    }
}
