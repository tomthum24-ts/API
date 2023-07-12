using API.DOMAIN;
using API.INFRASTRUCTURE.Interface;
using BaseCommon.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Project
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, MethodResult<DeleteProjectCommandResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IProjectRepository projectRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<MethodResult<DeleteProjectCommandResponse>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteProjectCommandResponse>();
            var existingUser = await _projectRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingUser == null || existingUser.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _projectRepository.DeleteRange(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            return methodResult;
        }
    }

}
