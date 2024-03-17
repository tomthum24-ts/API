using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseInDetail;
using API.INFRASTRUCTURE;
using AutoMapper;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
using BaseCommon.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.WareHouseIn
{
    public class UpdateWareHouseInCommandHandler : IRequestHandler<UpdateWareHouseInCommand, MethodResult<UpdateWareHouseInCommandResponse>>
    {
        private readonly IWareHouseInRepository _wareHouseInRepository;
        private readonly IWareHouseInDetailRepository _wareHouseInDetailRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWareHouseInCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWareHouseInRepository wareHouseInRepository, IWareHouseInDetailRepository wareHouseInDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _wareHouseInRepository = wareHouseInRepository;
            _wareHouseInDetailRepository = wareHouseInDetailRepository;
        }

        public async Task<MethodResult<UpdateWareHouseInCommandResponse>> Handle(UpdateWareHouseInCommand request, CancellationToken cancellationToken)
        {
            var methodResult = new MethodResult<UpdateWareHouseInCommandResponse>();
            var isExistData = await _wareHouseInRepository.Get(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
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

            foreach (var item in request.UpdateWareHouseIns)
            {
                if (item.Id == 0 && !item.IsDelete)
                {
                    var createDetail = new WareHouseInDetail(
                              isExistData.Id,
                              item.RangeOfVehicle,
                              item.QuantityVehicle,
                              item.ProductId,
                              item.QuantityProduct,
                              item.Unit,
                              item.Size,
                              item.Weight
                          );
                    _wareHouseInDetailRepository.Add(createDetail);
                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
                else if (item.IsDelete)
                {
                    var existingUser = await _wareHouseInDetailRepository.Get(x => x.Id == item.Id).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
                    _wareHouseInDetailRepository.Delete(existingUser);
                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    var existingDetail = await _wareHouseInDetailRepository.Get(x => x.Id == item.Id).FirstOrDefaultAsync(cancellationToken);
                    existingDetail.SetIdWareHouseIn(isExistData.Id);
                    existingDetail.SetRangeOfVehicle(item.RangeOfVehicle);
                    existingDetail.SetQuantityVehicle(item.QuantityVehicle);
                    existingDetail.SetProductId(item.ProductId);
                    existingDetail.SetQuantityProduct(item.QuantityProduct);
                    existingDetail.SetUnit(item.Unit);
                    existingDetail.SetSize(item.Size);
                    existingDetail.SetWeight(item.Weight);
                    await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            methodResult.Result = _mapper.Map<UpdateWareHouseInCommandResponse>(request);
            return methodResult;
        }
    }
}