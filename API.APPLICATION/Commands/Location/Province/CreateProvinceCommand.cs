

using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.Location.Province
{
    public class CreateProvinceCommand : IRequest<MethodResult<CreateProvinceCommandResponse>>
    {
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string CodeName { get; set; }
        public string DivisionType { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class CreateProvinceCommandResponse : CreateProvinceCommand
    {

    }
}
