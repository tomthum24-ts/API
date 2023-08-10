using API.Extension;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission.Role
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, MethodResult<DeleteRoleCommandResponse>>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public async Task<MethodResult<DeleteRoleCommandResponse>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteRoleCommandResponse>();
            var existingRole = await _rolePermissionRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingRole == null || existingRole.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _rolePermissionRepository.DeleteRange(existingRole);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            var dataResponses = _mapper.Map<List<DeleteRoleCommand>>(existingRole);
            methodResult.Result = new DeleteRoleCommandResponse(dataResponses);
            return methodResult;
        }
    }
}