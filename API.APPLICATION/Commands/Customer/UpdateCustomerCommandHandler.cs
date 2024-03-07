using API.DOMAIN;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Customer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, MethodResult<UpdateCustomerCommandResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICustomerRepository customerRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<MethodResult<UpdateCustomerCommandResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateCustomerCommandResponse>();
            var isExistData = await _customerRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }

            isExistData.SetCode(request.Code);
            isExistData.SetName(request.Name);
            isExistData.SetAddress(request.Address);
            isExistData.SetProvince(request.Province);
            isExistData.SetDistrict(request.District);
            isExistData.SetVillage(request.Village);
            isExistData.SetPhone(request.Phone);
            isExistData.SetPhone2(request.Phone2);
            isExistData.SetCMND(request.CMND);
            isExistData.SetBirthday(request.Birthday);
            isExistData.SetEmail(request.Email);
            isExistData.SetNote(request.Note);
            isExistData.SetTaxCode(request.TaxCode);
            isExistData.SetGroupMember(request.GroupMember);
            isExistData.SetFileAttach(request.FileAttach);
            isExistData.SetIsEnterprise(request.IsEnterprise);
            isExistData.SetEnterpriseName(request.EnterpriseName);
            isExistData.SetRepresentative(request.Representative);
            isExistData.SetPoisition(request.Poisition);
            isExistData.SetRegistrationNumber(request.RegistrationNumber);
            isExistData.SetRegistrationDate(request.RegistrationDate);
            isExistData.SetRegistrationAddress(request.RegistrationAddress);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<UpdateCustomerCommandResponse>(isExistData);
            return methodResult;
        }
    }
}