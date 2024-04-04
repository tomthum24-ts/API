using API.APPLICATION.Commands.Project;
using API.INFRASTRUCTURE.Interface.Notification;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using BaseCommon.Common.MethodResult;
using API.DOMAIN;
using API.DOMAIN.DomainObjects.Notification;
using BaseCommon.Common.ClaimUser;

namespace API.APPLICATION.Commands.Notification
{
    public class CreateNotificationTokenCommandHandler : IRequestHandler<CreateNotificationTokenCommand, MethodResult<CreateNotificationTokenCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly INotificationTokenRepository _notificationTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IUserSessionInfo _userSessionInfo;

        public CreateNotificationTokenCommandHandler(IUnitOfWork unitOfWork, INotificationTokenRepository notificationTokenRepository, IMapper mapper, IUserSessionInfo userSessionInfo)
        {
            _unitOfWork = unitOfWork;
            _notificationTokenRepository = notificationTokenRepository;
            _mapper = mapper;
            _userSessionInfo = userSessionInfo;
        }

        public async Task<MethodResult<CreateNotificationTokenCommandResponse>> Handle(CreateNotificationTokenCommand request, CancellationToken cancellationToken)
        {
            var IdUser = _userSessionInfo?.ID.Value ?? 0;
            var methodResult = new MethodResult<CreateNotificationTokenCommandResponse>();
            var isExistData = await _notificationTokenRepository.Get(x => x.UserName == request.UserName && x.TokenFireBase == request.TokenFireBase).FirstOrDefaultAsync(cancellationToken);
            if(isExistData == null)
            {
                
                var createUpdateToken = new NotificationToken(
                 request.TokenFireBase,
                 IdUser.ToString(),
                 request.UserName,
                 request.DeviceId,
                 request.Note
                );
                _notificationTokenRepository.Add(createUpdateToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                isExistData.SetTokenFireBase(request.TokenFireBase);
                isExistData.SetUserName(request.UserName);
                isExistData.SetNote(request.Note);
                isExistData.SetUserId(isExistData.Id.ToString());
                //isExistData.SetStatus(request.Status);
                _notificationTokenRepository.Update(isExistData);
                await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                methodResult.Result = _mapper.Map<CreateNotificationTokenCommandResponse>(request);
            }
            
            methodResult.Result = _mapper.Map<CreateNotificationTokenCommandResponse>(request);
            return methodResult;
        }
    }
}