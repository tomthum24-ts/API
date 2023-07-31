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

namespace API.APPLICATION.Commands.GroupPermission
{
    public class DeleteGroupPermissionCommandHandler : IRequestHandler<DeleteGroupPermissionCommand, MethodResult<DeleteGroupPermissionCommandResponse>>
    {
        private readonly IUserGroupPermissionRepository _userGroupPermissionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGroupPermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserGroupPermissionRepository userGroupPermissionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userGroupPermissionRepository = userGroupPermissionRepository;
        }

        public async Task<MethodResult<DeleteGroupPermissionCommandResponse>> Handle(DeleteGroupPermissionCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteGroupPermissionCommandResponse>();
            var existingGroupPermission = await _userGroupPermissionRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingGroupPermission == null || existingGroupPermission.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _userGroupPermissionRepository.DeleteRange(existingGroupPermission);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            var dataResponses = _mapper.Map<List<DeleteGroupPermissionCommand>>(existingGroupPermission);
            methodResult.Result = new DeleteGroupPermissionCommandResponse(dataResponses);
            return methodResult;
        }
    }
}