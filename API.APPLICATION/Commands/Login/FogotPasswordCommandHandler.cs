using API.APPLICATION.Commands.Login;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.EnCrypt;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.Extension;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Login
{
    public class FogotPasswordCommandHandler : IRequestHandler<FogotPasswordCommand, MethodResult<FogotPasswordCommandResponse>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FogotPasswordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<MethodResult<FogotPasswordCommandResponse>> Handle(FogotPasswordCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<FogotPasswordCommandResponse>();
            var isExistData = await _userRepository.Get(x => x.Email == request.Email).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Email)
                    });
                return methodResult;
            }
            try
            {
                
                var stringRandom = SendEmailExtension.RandomString(6, true).Trim();
                if (!string.IsNullOrEmpty(stringRandom))
                {
                    isExistData.SetPassWord(CommonBase.ToMD5(stringRandom.ToLower()));
                    _userRepository.Update(isExistData);
                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
               
                SendEmailExtension.SendMail(request.Email, "Mật khẩu mới của bạn là:  " + stringRandom.ToString(), "Cấp lại mật khẩu", "smtp.gmail.com", 587);
            }
            catch (System.Exception)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB12), new[]
                  {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Email)
                    });
                return methodResult;
            }
           
            
            return methodResult;
        }
    }
}
