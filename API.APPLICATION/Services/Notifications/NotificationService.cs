using API.APPLICATION.ViewModels.Notification;
using CorePush.Google;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static API.APPLICATION.ViewModels.Notification.GoogleNotification;
using API.INFRASTRUCTURE.DataConnect;
using Dapper;

namespace API.APPLICATION.Services.Notifications
{
    public interface INotificationService
    {
        Task<ResponseModel> SendNotification(NotificationModel notificationModel);
        Task<ResponseModel> SendNotiToAdmin(NotificationModel notificationModel);
    }

    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public readonly DapperContext _context;
        public NotificationService(IOptions<FcmNotificationSetting> settings, DapperContext context)
        {
            _fcmNotificationSetting = settings.Value;
            _context = context;
        }

        public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            try
            {
               
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _fcmNotificationSetting.SenderId,
                        ServerKey = _fcmNotificationSetting.ServerKey
                    };
                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    DataPayload dataPayload = new DataPayload();
                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;
                    dataPayload.URL = notificationModel.URL;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;
                    notification.URL = dataPayload.URL;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                
               
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }
        public async Task<ResponseModel> SendNotiToAdmin(NotificationModel notificationModel)
        {
            ResponseModel response = new ResponseModel();
            var regUser = new RequestUserReceiveModel();
            regUser.IsAdmin = true;
            var lstUser = await GetListReceiveNotiAsync(regUser).ConfigureAwait(false);
            if (lstUser != null)
            {
                foreach (var item in lstUser)
                {
                    notificationModel.DeviceId = item.TokenFireBase;
                    var sendTo = SendNotification(notificationModel);
                }
            }

            return response;
        }
        public async Task<IEnumerable<ReponseUserReceiveNotiModel>> GetListReceiveNotiAsync(RequestUserReceiveModel param)
        {
            var conn = _context.CreateConnection();
            using var rs = await conn.QueryMultipleAsync("SP_NT_GetListNotification", param, commandType: System.Data.CommandType.StoredProcedure);
            var result = await rs.ReadAsync<ReponseUserReceiveNotiModel>().ConfigureAwait(false);
            return result;
        }

    }
}
