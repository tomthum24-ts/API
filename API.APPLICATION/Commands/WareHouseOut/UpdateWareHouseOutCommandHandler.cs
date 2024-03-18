using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseOutDetail;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseOut
{
    public class UpdateWareHouseOutCommandHandler : IRequestHandler<UpdateWareHouseOutCommand, MethodResult<UpdateWareHouseOutCommandResponse>>
    {
        private readonly IWareHouseOutRepository _WareHouseOutRepository;
        private readonly IWareHouseOutDetailRepository _WareHouseOutDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWareHouseOutCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWareHouseOutRepository WareHouseOutRepository, IWareHouseOutDetailRepository WareHouseOutDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _WareHouseOutRepository = WareHouseOutRepository;
            _WareHouseOutDetailRepository = WareHouseOutDetailRepository;
        }

        public async Task<MethodResult<UpdateWareHouseOutCommandResponse>> Handle(UpdateWareHouseOutCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateWareHouseOutCommandResponse>();
            var isExistData = await _WareHouseOutRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (isExistData == null || isExistData.Id < 0)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB02), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(User), request.Id)
                    });
                return methodResult;
            }

            isExistData.SetCode(request.Code);
            isExistData.SetDateCode(request.DateCode);
            isExistData.SetCustomerID(request.CustomerID);
            isExistData.SetRepresentative(request.Representative);
            isExistData.SetIntendTime(request.IntendTime);
            isExistData.SetWareHouse(request.WareHouse);
            isExistData.SetNote(request.Note);
            isExistData.SetOrtherNote(request.OrtherNote);
            isExistData.SetFileAttach(request.FileAttach);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            foreach (var item in request.UpdateWareHouseOuts)
            {
                if (item.Id == 0 && !item.IsDelete)
                {
                    var createDetail = new WareHouseOutDetail(
                              isExistData.Id,
                              item.RangeOfVehicle,
                              item.QuantityVehicle,
                              item.ProductId,
                              item.QuantityProduct,
                              item.Unit,
                              item.Size,
                              item.Weight,
                              item.RONumber
                          );
                    _WareHouseOutDetailRepository.Add(createDetail);
                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
                else if (item.IsDelete)
                {
                    var existingItem = await _WareHouseOutDetailRepository.Get(x => x.Id == item.Id).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                    if(existingItem != null) {
                        _WareHouseOutDetailRepository.Delete(existingItem);
                        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    }
                    
                }
                else
                {
                    var existingDetail = await _WareHouseOutDetailRepository.Get(x => x.Id == item.Id).FirstOrDefaultAsync(cancellationToken);
                    existingDetail.SetIdWareHouseOut(isExistData.Id);
                    existingDetail.SetRangeOfVehicle(item.RangeOfVehicle);
                    existingDetail.SetQuantityVehicle(item.QuantityVehicle);
                    existingDetail.SetProductId(item.ProductId);
                    existingDetail.SetQuantityProduct(item.QuantityProduct);
                    existingDetail.SetUnit(item.Unit);
                    existingDetail.SetSize(item.Size);
                    existingDetail.SetWeight(item.Weight);
                    existingDetail.SetRONumber(item.RONumber);
                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            methodResult.Result = _mapper.Map<UpdateWareHouseOutCommandResponse>(request);
            return methodResult;
        }
    }
}