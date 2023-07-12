using API.Extension;
using API.INFRASTRUCTURE;
using BaseCommon.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;


namespace API.APPLICATION.Commands.User
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, MethodResult<ChangePasswordCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IUserSessionInfo _userSessionInfo;

        public ChangePasswordCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IUserSessionInfo userSessionInfo)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userSessionInfo = userSessionInfo;
        }

        public async Task<MethodResult<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<ChangePasswordCommandResponse>();
            var id = _userSessionInfo.ID.GetValueOrDefault();
            var editEntity = await _userRepository.Get(x=>x.Id== id).FirstOrDefaultAsync(cancellationToken);
            bool item = await _userRepository.Get(x => x.Id == id).AnyAsync(cancellationToken);
            string errorMessage = "";
            
            if (!item)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), id),
                        errorMessage
                    });
                return methodResult;

            }
            bool strongPass = CommonBase.IsStrongPassword(request.Password, out errorMessage);
            if (!strongPass)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB03), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), id),
                        errorMessage
                    });
                return methodResult;

            }
            editEntity.SetPassWord(CommonBase.ToMD5(request.Password));
            _userRepository.Update(editEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false); ;
            //methodResult.Result = _mapper.Map<ChangePasswordCommandResponse>(editEntity);
            return methodResult;
        }
    }
}
