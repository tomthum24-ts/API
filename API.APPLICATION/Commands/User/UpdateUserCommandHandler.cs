using BaseCommon.Common.MethodResult;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using API.HRM.DOMAIN;
using API.INFRASTRUCTURE;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using API.INFRASTRUCTURE.Interface.UnitOfWork;
using BaseCommon.Enums;

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
            var isExistData = await _userRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }
            bool existingUser = await _userRepository.Get(x => x.UserName == request.UserName && x.Id != request.Id).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.UserName), request.UserName)
                    });
                return methodResult;
            }
            isExistData.SetUserName(request.UserName);
            isExistData.SetName(request.Name);
            isExistData.SetLastName(request.LastName);
            isExistData.SetEmail(request.Email);
            isExistData.SetAddress(request.Address);
            isExistData.SetPhone(request.Phone);
            isExistData.SetDepartment(request.Department);
            isExistData.SetBirthDay(request.BirthDay);
            isExistData.SetProvince(request.Province);
            isExistData.SetDistrict(request.District);
            isExistData.SetVillage(request.Village);
            isExistData.SetProject(request.Project);
            isExistData.SetNote(request.Note);
            isExistData.SetStatus(request.Status);
            _userRepository.Update(isExistData);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateUserCommandResponse>(isExistData);
            return methodResult;
        }
    }
}
