using API.APPLICATION.Commands.RolePermission.Credential;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface;
using BaseCommon.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateCredentialCommandHandler : IRequestHandler<CreateCredentialCommand, MethodResult<CreateCredentialCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ICredentialRepository _credentialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCredentialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICredentialRepository credentialRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<MethodResult<CreateCredentialCommandResponse>> Handle(CreateCredentialCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateCredentialCommandResponse>();
            var createCredential = new PM_Credential(
                 request.UserGroupId,
                 request.RoleId,
                 request.Note,
                 request.Status
                );
            _credentialRepository.Add(createCredential);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateCredentialCommandResponse>(createCredential);
            return methodResult;
        }
    }
}
