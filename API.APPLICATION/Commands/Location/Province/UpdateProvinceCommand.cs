using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.Location.Province
{
    public class UpdateProvinceCommand : IRequest<MethodResult<UpdateProvinceCommandResponse>>
    {
        public int Id { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string CodeName { get; set; }
        public string DivisionType { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateProvinceCommandResponse : UpdateProvinceCommand
    {
    }
}
