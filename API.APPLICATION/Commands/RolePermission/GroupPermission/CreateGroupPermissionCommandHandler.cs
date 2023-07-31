using API.DOMAIN.DomainObjects.Permission;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.GroupPermission
{
    public class CreateUserGroupPermissionCommandHandler : IRequestHandler<CreateGroupPermissionCommand, MethodResult<CreateGroupPermissionCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserGroupPermissionRepository _UserGroupPermissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserGroupPermissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserGroupPermissionRepository UserGroupPermissionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UserGroupPermissionRepository = UserGroupPermissionRepository;
        }

        public async Task<MethodResult<CreateGroupPermissionCommandResponse>> Handle(CreateGroupPermissionCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateGroupPermissionCommandResponse>();
            var createUserGroupPermission = new UserGroupPermissions(
                 request.Name,
                 request.Note,
                 request.Status
                );
            _UserGroupPermissionRepository.Add(createUserGroupPermission);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateGroupPermissionCommandResponse>(createUserGroupPermission);
            return methodResult;
        }
    }
}