using API.APPLICATION.Commands.WareHouseIn;
using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseInDetail;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.UnitOfWork;
using MediatR;
using System.Collections.Generic;
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
            //bool existingCode = await _wareHouseInRepository.Get(x => x.Code == request.Code).AnyAsync(cancellationToken);
            //if (existingCode)
            //{
            //    methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
            //        {
            //            ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
            //        });
            //    return methodResult;
            //}
            var createWareHouse = new WareHouseIn(
                request.DateCode,
                request.CustomerID,
                request.Representative,
                request.IntendTime,
                request.WareHouse,
                request.Note,
                request.OrtherNote,
                request.FileAttach
                );
            _wareHouseInRepository.Add(createWareHouse);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            List<WareHouseInDetail> lstDetail = new List<WareHouseInDetail>();
            foreach (var item in request.WareHouseInDetail)
            {
                var createDetail = new WareHouseInDetail(
                                createWareHouse.Id,
                                item.RangeOfVehicle,
                                item.QuantityVehicle,
                                item.ProductId,
                                item.QuantityProduct,
                                item.Unit,
                                item.Size,
                                item.Weight
                            );
                lstDetail.Add(createDetail);
            }
            _wareHouseInDetailRepository.AddRange(lstDetail);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateWareHouseInCommandResponse>(request);
            return methodResult;
        }
    }
}