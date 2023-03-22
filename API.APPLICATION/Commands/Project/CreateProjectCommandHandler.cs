using API.APPLICATION.Commands.Project;
using API.APPLICATION.Commands.User;
using API.DOMAIN;
using API.INFRASTRUCTURE.Interface;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, MethodResult<CreateProjectCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProjectCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
        }


        public async Task<MethodResult<CreateProjectCommandResponse>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateProjectCommandResponse>();
            bool existingUser = await _projectRepository.Get(x => x.ProjectCode == request.ProjectCode).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.ProjectCode), request.ProjectCode)
                    });
                return methodResult;
            }
            var createProject = new Project(
                 request.ProjectCode,
                 request.ProjectName,
                 request.Note,
                 request.Status
                );
            _projectRepository.Add(createProject);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateProjectCommandResponse>(createProject);
            return methodResult;
        }
    }
}
