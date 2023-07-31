using API.Extension;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.GroupPermission
{
    public class UpdateGroupPermissionCommandHandler : IRequestHandler<UpdateGroupPermissionCommand, MethodResult<UpdateGroupPermissionCommandResponse>>
    {
        private readonly IUserGroupPermissionRepository _userGroupPermissionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGroupPermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserGroupPermissionRepository userGroupPermissionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userGroupPermissionRepository = userGroupPermissionRepository;
        }

        public async Task<MethodResult<UpdateGroupPermissionCommandResponse>> Handle(UpdateGroupPermissionCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateGroupPermissionCommandResponse>();
            var isExistData = await _userGroupPermissionRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }
            isExistData.SetName(request.Name);
            isExistData.SetNote(request.Note); 
            isExistData.SetStatus(request.Status);
            _userGroupPermissionRepository.Update(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateGroupPermissionCommandResponse>(isExistData);
            return methodResult;
        }
    }
}