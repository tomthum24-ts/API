using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;

namespace API.APPLICATION.Commands.SysBieuMau
{
    public class DeleteSysBieuMauCoQuanCommand : IRequest<MethodResult<DeleteSysBieuMauCoQuanCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }

    public class DeleteSysBieuMauCoQuanCommandResponse
    { }
}