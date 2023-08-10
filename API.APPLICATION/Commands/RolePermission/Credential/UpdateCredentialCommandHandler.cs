using API.DOMAIN;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.RolePermission.Credential
{
    public class UpdateCredentialCommandHandler : IRequestHandler<UpdateCredentialCommand, MethodResult<IEnumerable<UpdateCredentialCommandResponse>>>
    {
        private readonly ICredentialRepository _credentialRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCredentialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICredentialRepository credentialRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _credentialRepository = credentialRepository;
        }

        public async Task<MethodResult<IEnumerable<UpdateCredentialCommandResponse>>> Handle(UpdateCredentialCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<IEnumerable<UpdateCredentialCommandResponse>>();
       
            return methodResult;
        }
    }
}