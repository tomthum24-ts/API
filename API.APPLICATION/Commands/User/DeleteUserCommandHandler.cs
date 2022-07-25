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
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, MethodResult<DeleteUserCommandResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<MethodResult<DeleteUserCommandResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<DeleteUserCommandResponse>();
            var existingUser = await _userRepository.Get(x => request.Ids.Contains(x.Id)).ToListAsync(cancellationToken).ConfigureAwait(false);
            if (existingUser == null || existingUser.Count == 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Ids)
                    });
                return methodResult;
            }
            _userRepository.DeleteRange(existingUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            //methodResult.Result = _mapper.Map<DeleteUserCommandResponse>(existingUser);
            return methodResult;
        }
    }
}
