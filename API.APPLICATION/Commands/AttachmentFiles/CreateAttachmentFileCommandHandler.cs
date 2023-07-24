

using API.APPLICATION.ViewModels.Media;
using API.DOMAIN;
using API.INFRASTRUCTURE.Interface.Media;
using AutoMapper;
using BaseCommon.Authorization;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.MethodResult;
using BaseCommon.UnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.AttachmentFiles
{
   
    public class CreateAttachmentFileCommandHandler : IRequestHandler<CreateAttachmentFileCommand, MethodResult<CreateAttachmentFileResponse>>
    {
        private readonly IAttachmentFileRepository _attachmentFileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserSessionInfo _userSessionInfo;
        private readonly IPermissionChecker _permissionChecker;
        public CreateAttachmentFileCommandHandler(IMapper mapper, IAttachmentFileRepository attachmentFileRepository, IUnitOfWork unitOfWork, IUserSessionInfo userSessionInfo, IPermissionChecker permissionChecker)
        {
            _mapper = mapper;
            _attachmentFileRepository = attachmentFileRepository;
            _unitOfWork = unitOfWork;
            _userSessionInfo = userSessionInfo;
            _permissionChecker = permissionChecker;
        }

        public async Task<MethodResult<CreateAttachmentFileResponse>> Handle(CreateAttachmentFileCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateAttachmentFileResponse>();

            List<AttachmentFile> newAttachmentFiles = new List<AttachmentFile>();

            if (!request.Files.Any()) return methodResult;

            foreach (var item in request.Files)
            {
                newAttachmentFiles.Add(new AttachmentFile(item.Name, item.Type, item.Path+"/"+ item.Name, item.Size));
            }

            _attachmentFileRepository.AddRange(newAttachmentFiles);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var attachmentFileResponses = _mapper.Map<List<MediaResponse>>(newAttachmentFiles);

            methodResult.Result = new CreateAttachmentFileResponse(attachmentFileResponses);

            return methodResult;
        }
    }
}