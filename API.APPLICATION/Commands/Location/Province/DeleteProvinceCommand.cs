using BaseCommon.Common.MethodResult;
using MediatR;
using System.Collections.Generic;


namespace API.APPLICATION.Commands.Location.Province
{
    public class DeleteProvinceCommand : IRequest<MethodResult<DeleteProvinceCommandResponse>>
    {
        public List<int> Ids { get; set; }
    }
    public class DeleteProvinceCommandResponse { }
}
