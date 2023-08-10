using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.UnitOfWork;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission.Role
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, MethodResult<UpdateRoleCommandResponse>>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IRolePermissionRepository rolePermissionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<MethodResult<UpdateRoleCommandResponse>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateRoleCommandResponse>();
            var isExistData = _rolePermissionRepository.Get(x => x.Id == request.Id).FirstOrDefault();
            isExistData.SetNameController(request.NameController);
            isExistData.SetActionName(request.ActionName);
            isExistData.SetNote(request.Note);
            isExistData.SetStatus(request.Status);
            _rolePermissionRepository.Update(isExistData);

            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateRoleCommandResponse>(isExistData);
            return methodResult;
        }
    }
}