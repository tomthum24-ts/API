using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.Location.SyncLocation
{
    public class SyncLocationCommand : IRequest<MethodResult<SyncLocationCommandResponse>>
    {
        public int PhanLoai { get; set; }
    }
    public class SyncLocationCommandResponse
    {
      
    }
}
