using API.APPLICATION.Commands.WareHouseIn;
using API.APPLICATION.Services.Notifications;
using API.APPLICATION.ViewModels.Notification;
using API.DOMAIN;
using API.DOMAIN.DomainObjects.WareHouseInDetail;
using API.DOMAIN.DomainObjects.WareHouseInFileAttach;
using API.DOMAIN.DomainObjects.WareHouseOutFileAttach;
using API.INFRASTRUCTURE;
using API.INFRASTRUCTURE.Interface;
using API.INFRASTRUCTURE.Repositories;
using AutoMapper;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.MethodResult;
using BaseCommon.Enums;
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
        private readonly INotificationService _notificationService;
        private readonly IWareHouseInFileAttachRepository  _wareHouseInFileAttachRepository;
        private readonly IUserSessionInfo _userSessionInfo;

        public CreateWareHouseInCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IWareHouseInRepository wareHouseInRepository, IWareHouseInDetailRepository wareHouseInDetailRepository, INotificationService notificationService, IWareHouseInFileAttachRepository wareHouseInFileAttachRepository, IUserSessionInfo userSessionInfo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _wareHouseInRepository = wareHouseInRepository;
            _wareHouseInDetailRepository = wareHouseInDetailRepository;
            _notificationService = notificationService;
            _wareHouseInFileAttachRepository = wareHouseInFileAttachRepository;
            _userSessionInfo = userSessionInfo;
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
            var userCreate = _userSessionInfo.ID;
            var createWareHouse = new WareHouseIn(
                    request.Code,
                    request.DateCode,
                    //request.CustomerID,
                    userCreate,
                    request.Representative,
                    request.IntendTime,
                    request.WareHouse,
                    request.CustomerName,
                    request.FilePath,
                    request.FileName,
                    request.Seal,
                    request.Temp,
                    request.CarNumber,
                    request.Container,
                    request.Door,
                    request.Deliver,
                    request.Veterinary,
                    request.Cont,
                    request.Note,
                    request.OrtherNote,
                    request.FileAttach,
                    request.NumberCode,
                    request.InvoiceNumber,
                    request.TimeStart,
                    request.TimeEnd,
                    request.Pallet,
                    request.AddressCustomer,
                    request.Require
                );
            _wareHouseInRepository.Add(createWareHouse);
            var idWareHouse = await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            List<WareHouseInDetail> lstDetail = new List<WareHouseInDetail>();
            if (request.WareHouseInDetail.Count < 1)
            {
                methodResult.AddAPIErrorMessage(nameof(EErrorCode.EB07), new[]
                    {
                        ErrorHelpers.GenerateErrorResult(nameof(request.Code), request.Code)
                    });
                return methodResult;
            }
            foreach (var item in request.WareHouseInDetail)
            {
                foreach (var item2 in item.VehicleDetails)
                {
                    var createDetail = new WareHouseInDetail(
                        createWareHouse.Id,
                        item.RangeOfVehicle,
                        null,
                        item2.ProductId,
                        item2.QuantityProduct,
                        item2.Unit,
                        item2.Size,
                        item2.Weight,
                        item.GuildId,
                        item2.Note,
                        item2.LotNo,
                        item2.TotalWeighScan,
                        item2.ProductDate,
                        item2.ExpiryDate,
                        item2.MadeIn,
                        item2.ProductName,
                        item2.UnitName,
                        item.ContainerNumber,
                        item.VehicleNumber
                            );
                    lstDetail.Add(createDetail);
                }
            }
            _wareHouseInDetailRepository.AddRange(lstDetail);

            List<WareHouseInFileAttachs> WareHouseInFileAttach = new List<WareHouseInFileAttachs>();
            foreach (var file in request.WareHouseInFileAttachs)
            {
                var createFile = new WareHouseInFileAttachs(
                        createWareHouse.Id,
                        file.Name,
                        file.Path
                    );
                WareHouseInFileAttach.Add(createFile);
            }
            _wareHouseInFileAttachRepository.AddRange(WareHouseInFileAttach);
            await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            methodResult.Result = _mapper.Map<CreateWareHouseInCommandResponse>(request);
            try
            {
                var noti = new NotificationModel();
                noti.Title = "Đơn hàng nhập mới";
                noti.Body = "Đơn hàng  " + request.Code+ "vừa được tạo";
                noti.URL.ModuleName = "Nhập kho";
                noti.URL.ModuleType = (int)TypeNotifyEnum.WareHouseIn;
                noti.URL.Id = idWareHouse;
                _notificationService.SendNotiToAdmin(noti);
            }
            catch (System.Exception)
            {


            }
            return methodResult;
        }
    }
}