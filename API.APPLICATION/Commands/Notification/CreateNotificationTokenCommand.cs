using BaseCommon.Common.MethodResult;
using MediatR;
using System.Text.Json.Serialization;

namespace API.APPLICATION.Commands.Notification
{
    public class CreateNotificationTokenCommand : IRequest<MethodResult<CreateNotificationTokenCommandResponse>>
    {
        public string TokenFireBase { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        public string UserName { get; set; }

        [JsonIgnore]
        public string DeviceId { get; set; }

        public string Note { get; set; }
    }

    public class CreateNotificationTokenCommandResponse : CreateNotificationTokenCommand
    {
    }
}