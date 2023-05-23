
using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.RefreshTooken
{
    public class UpdateRefreshTookenCommand : IRequest<MethodResult<UpdateRefreshTookenCommandResponse>>
    {
    }
    public class UpdateRefreshTookenCommandResponse : UpdateRefreshTookenCommand
    {
    }
}
