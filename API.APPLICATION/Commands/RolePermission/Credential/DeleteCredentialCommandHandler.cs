using API.DOMAIN;
using API.INFRASTRUCTURE;
using BaseCommon.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission.Credential
{
    public class DeleteCredentialCommandHandler : IRequestHandler<DeleteCredentialCommand, MethodResult<DeleteCredentialCommandResponse>>
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCredentialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICredentialRepository credentialRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<MethodResult<DeleteCredentialCommandResponse>> Handle(DeleteCredentialCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteCredentialCommandResponse>();
            var existingCredential = await _credentialRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingCredential == null || existingCredential.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _credentialRepository.DeleteRange(existingCredential);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            var dataResponses = _mapper.Map<List<DeleteCredentialCommand>>(existingCredential);
            methodResult.Result = new DeleteCredentialCommandResponse(dataResponses);
            return methodResult;
        }
    }
}
