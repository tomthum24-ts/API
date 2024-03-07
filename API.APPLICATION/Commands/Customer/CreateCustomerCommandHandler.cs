using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Commands.User;
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

namespace API.APPLICATION
{
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, MethodResult<CreateCustomerCommandResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MethodResult<CreateCustomerCommandResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateCustomerCommandResponse>();
            bool existingUser = await _customerRepository.Get(x => x.Phone == request.Phone).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Phone), request.Phone)
                    });
                return methodResult;
            }
            var createCustomer = new Customer(
                    request.Code,
                    request.Name,
                    request.Address,
                    request.Province,
                    request.District,
                    request.Village,
                    request.Phone,
                    request.Phone2,
                    request.CMND,
                    request.Birthday,
                    request.Email,
                    request.Note,
                    request.TaxCode,
                    request.GroupMember,
                    request.FileAttach,
                    request.IsEnterprise,
                    request.EnterpriseName,
                    request.Representative,
                    request.Poisition,
                    request.RegistrationNumber,
                    request.RegistrationDate,
                    request.RegistrationAddress

                );
            _customerRepository.Add(createCustomer);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateCustomerCommandResponse>(createCustomer);
            return methodResult;
        }
    }
}