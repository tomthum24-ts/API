
using BaseCommon.Common.MethodResult;
using MediatR;
using System.Text.Json.Serialization;

namespace API.APPLICATION.Commands.RefreshToken
{
    public class RevokeTokenCommand : IRequest<MethodResult<RevokeTokenCommandResponse>>
    {
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public bool IsLogout { get; set; }
    }

    public class RevokeTokenCommandResponse : RevokeTokenCommand
    {
       
    }
}
