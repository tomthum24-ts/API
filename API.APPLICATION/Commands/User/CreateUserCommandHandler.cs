using API.APPLICATION.Commands.User;
using API.HRM.DOMAIN;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.DataConnect;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MethodResult<CreateUserCommandResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _user;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandHandler( IMapper mapper, IUserService user, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _user = user;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<MethodResult<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateUserCommandResponse>();
            bool existingUser =await _userRepository.Get(x=>x.UserName==request.UserName).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EBaseErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.UserName), request.UserName)
                    });
                return methodResult;
            }
            var createUser = new User(
                 request.UserName,
                 request.Name,
                 request.Address,
                 request.Phone,
                 request.Status
                );
            _userRepository.Add(createUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateUserCommandResponse>(createUser);
            return methodResult;
        }


    }
}
