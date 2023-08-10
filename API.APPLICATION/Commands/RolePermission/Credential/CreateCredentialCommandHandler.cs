using API.APPLICATION.Commands.RolePermission.Credential;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateCredentialCommandHandler : IRequestHandler<CreateCredentialCommand, MethodResult<IEnumerable<CreateCredentialCommandResponse>>>
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

        public async Task<MethodResult<IEnumerable<CreateCredentialCommandResponse>>> Handle(CreateCredentialCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<IEnumerable<CreateCredentialCommandResponse>>();
            var lstCreate = request.CreateCredentials?.ToList();
            //Xóa quyền cũ
            var idGroup = await _credentialRepository.Get(x => x.UserGroupId == request.UserGroupId).Select(y => y.Id).ToListAsync(cancellationToken).ConfigureAwait(false);
            var existingCredential = await _credentialRepository.Get(x => idGroup.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);

            //Add quyền mới
            List<PM_Credential> lstCredentials = new List<PM_Credential>();
            if (lstCreate != null)
            {
                foreach (var item in lstCreate)
                {
                    var createCredential = new PM_Credential(
                    request.UserGroupId,
                    item
                    );
                    lstCredentials.Add(createCredential);
                }
                _credentialRepository.AddRange(lstCredentials);
            }
            _credentialRepository.DeleteRange(existingCredential);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<IEnumerable<CreateCredentialCommandResponse>>(lstCredentials);
            return methodResult;
        }
    }
}