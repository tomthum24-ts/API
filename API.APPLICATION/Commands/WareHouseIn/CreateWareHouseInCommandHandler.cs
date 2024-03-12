using API.APPLICATION.Commands.Customer;
using API.APPLICATION.Commands.WareHouseIn;
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
    public class CreateWareHouseInCommandHandler : IRequestHandler<CreateWareHouseInCommand, MethodResult<CreateWareHouseInCommandResponse>>
    {
        private readonly IWareHouseInRepository _wareHouseInRepository;
        private readonly IWareHouseInDetailRepository _wareHouseInDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWareHouseInCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IWareHouseInRepository wareHouseInRepository, IWareHouseInDetailRepository wareHouseInDetailRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _wareHouseInRepository = wareHouseInRepository;
            _wareHouseInDetailRepository = wareHouseInDetailRepository;
        }

        public async Task<MethodResult<CreateWareHouseInCommandResponse>> Handle(CreateWareHouseInCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateWareHouseInCommandResponse>();
            bool existingUser = await _wareHouseInRepository.Get(x => x.Code == request.Code).AnyAsync(cancellationToken);
            if (existingUser)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
                    });
                return methodResult;
            }
            var createCustomer = new WareHouseIn(
                request.Code,
                request.DateCode,
                request.CustomerID,
                request.Representative,
                request.IntendTime,
                request.WareHouse,
                request.Note,
                request.OrtherNote,
                request.FileAttach
                );
            _wareHouseInRepository.Add(createCustomer);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateWareHouseInCommandResponse>(createCustomer);
            return methodResult;
        }
    }
}