using API.APPLICATION.Commands.User;
using API.DOMAIN;
using API.INFRASTRUCTURE;
using BaseCommon.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;

using BaseCommon.UnitOfWork;

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using BaseCommon.Extension;
using BaseCommon.Common.EnCrypt;

namespace API.APPLICATION
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MethodResult<CreateUserCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserServices _user;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IMapper mapper, IUserServices user, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _user = user;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MethodResult<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateUserCommandResponse>();
            bool existingEmail = await _userRepository.Get(x => x.Email == request.Email && x.IsDelete != true).AnyAsync(cancellationToken);
            if (existingEmail)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB11), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Email), request.Email)
                    });
                return methodResult;
            }
            if (request.Email == "" || request.PassWord == "")
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB09), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Email), request.Email),
                        ErrorHelpers.GenerateErrorResult(nameof(request.PassWord), request.PassWord)
                    });
                return methodResult;
            }
            var stringRandom = SendEmailExtension.RandomString(6, true).Trim();
            var createUser = new User(
                 request.Email.ToLower(),
                 CommonBase.ToMD5(request.PassWord),
                 request.Name,
                 request.LastName,
                 request.Email,
                 request.Address,
                 request.Phone,
                 request.Department,
                 request.BirthDay,
                 request.Province,
                 request.District,
                 request.Village,
                 request.Project,
                 request.Note,
                 request.Status == false,
                 stringRandom
                );
            _userRepository.Add(createUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                SendEmailExtension.SendMail(request.Email, "Otp của bạn là: " + stringRandom.ToString(), "Kích hoạt tài khoản VietColdChain " , "smtp.gmail.com", 587);

            }
            catch (System.Exception)
            {

                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB12), new[]
                  {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Email)
                    });
                return methodResult;
            }
          
            methodResult.Result = _mapper.Map<CreateUserCommandResponse>(createUser);
            return methodResult;
        }
    }
}