using API.DOMAIN;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission.Credential
{
    public class UpdateCredentialCommandHandler : IRequestHandler<UpdateCredentialCommand, MethodResult<UpdateCredentialCommandResponse>>
    {
        private readonly ICredentialRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCredentialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICredentialRepository projectRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public async Task<MethodResult<UpdateCredentialCommandResponse>> Handle(UpdateCredentialCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateCredentialCommandResponse>();
            var isExistData = await _projectRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }
            isExistData.SetRoleId(request.RoleId);
            isExistData.SetUserGroupId(request.UserGroupId);
            isExistData.SetNote(request.Note); ;
            isExistData.SetStatus(request.Status);
            _projectRepository.Update(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateCredentialCommandResponse>(isExistData);
            return methodResult;
        }
    }
}
