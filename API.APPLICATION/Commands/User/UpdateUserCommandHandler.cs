using API.INFRASTRUCTURE.DataConnect;
using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http.Extensions;
using API.HRM.DOMAIN;
using static BaseCommon.Enums.ErrorCodesEnum;
using API.INFRASTRUCTURE;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API.INFRASTRUCTURE.Interface.UnitOfWork;

namespace API.APPLICATION.Commands.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MethodResult<UpdateUserCommandResponse>>
    {
   
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<MethodResult<UpdateUserCommandResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateUserCommandResponse>();
            var editEntity = await _userRepository.Get(x => x.Id == request.id).FirstOrDefaultAsync(cancellationToken);
            if (editEntity == null || editEntity.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EBaseErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.id)
                    });
                return methodResult;
            }
            bool existingUser = await _userRepository.Get(x => x.UserName == request.UserName).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EBaseErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.UserName), request.UserName)
                    });
                return methodResult;
            }
            editEntity.SetName(request.Name);
            editEntity.SetUserName(request.UserName);
            editEntity.SetAddress(request.Address);
            editEntity.SetPhone(request.Phone);
            editEntity.SetStatus(request.Status);
            _userRepository.Update(editEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateUserCommandResponse>(editEntity);
            return methodResult;
        }
    }
}
