using API.Extension;
using API.HRM.DOMAIN;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
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

        public ChangePasswordCommandHandler( IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MethodResult<ChangePasswordCommandResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<ChangePasswordCommandResponse>();
            var editEntity = await _userRepository.Get(x=>x.Id==request.id).FirstOrDefaultAsync(cancellationToken);
            bool item = await _userRepository.Get(x => x.Id == request.id).AnyAsync(cancellationToken);
            string errorMessage = "";
            
            if (!item)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.id),
                        errorMessage
                    });
                return methodResult;

            }
            bool strongPass = CommonBase.IsStrongPassword(request.Password, out errorMessage);
            if (!strongPass)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB03), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.id),
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
