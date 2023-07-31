using API.APPLICATION.Commands.RolePermission.Role;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRolePermissionCommand, MethodResult<CreateRolePermissionCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IRolePermissionRepository rolePermissionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<MethodResult<CreateRolePermissionCommandResponse>> Handle(CreateRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateRolePermissionCommandResponse>();
            var createRole = new RolePermissions(
                 request.NameController,
                 request.ActionName,
                 request.Note,
                 request.Status
                );
            _rolePermissionRepository.Add(createRole);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateRolePermissionCommandResponse>(createRole);
            return methodResult;
        }
    }
}