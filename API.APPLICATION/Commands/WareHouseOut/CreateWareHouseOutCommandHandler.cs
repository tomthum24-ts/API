using API.APPLICATION.Commands.WareHouseOut;
using API.DOMAIN.DomainObjects.WareHouseOut;
using API.DOMAIN.DomainObjects.WareHouseOutDetail;
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
    public class CreateWareHouseOutCommandHandler : IRequestHandler<CreateWareHouseOutCommand, MethodResult<CreateWareHouseOutCommandResponse>>
    {
        private readonly IWareHouseOutRepository _WareHouseOutRepository;
        private readonly IWareHouseOutDetailRepository _WareHouseOutDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWareHouseOutCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IWareHouseOutRepository WareHouseOutRepository, IWareHouseOutDetailRepository WareHouseOutDetailRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _WareHouseOutRepository = WareHouseOutRepository;
            _WareHouseOutDetailRepository = WareHouseOutDetailRepository;
        }

        public async Task<MethodResult<CreateWareHouseOutCommandResponse>> Handle(CreateWareHouseOutCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<CreateWareHouseOutCommandResponse>();
            //bool existingCode = await _WareHouseOutRepository.Get(x => x.Code == request.Code).AnyAsync(cancellationToken);
            //if (existingCode)
            //{
            //    methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB01), new[]
            //        {
            //            ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
            //        });
            //    return methodResult;
            //}
            var createWareHouse = new WareHouseOut(
                request.DateCode,
                request.CustomerID,
                request.Representative,
                request.IntendTime,
                request.WareHouse,
                request.Note,
                request.OrtherNote,
                request.FileAttach
                );
            _WareHouseOutRepository.Add(createWareHouse);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            List<WareHouseOutDetail> lstDetail = new List<WareHouseOutDetail>();
            foreach (var item in request.WareHouseOutDetail)
            {
                var createDetail = new WareHouseOutDetail(
                                createWareHouse.Id,
                                item.RangeOfVehicle,
                                item.QuantityVehicle,
                                item.ProductId,
                                item.QuantityProduct,
                                item.Unit,
                                item.Size,
                                item.Weight,
                                item.RONumber
                            );
                lstDetail.Add(createDetail);
            }
            _WareHouseOutDetailRepository.AddRange(lstDetail);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateWareHouseOutCommandResponse>(request);
            return methodResult;
        }
    }
}