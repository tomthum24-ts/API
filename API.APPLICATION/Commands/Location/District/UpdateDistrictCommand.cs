
using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.Location.District
{
    public class UpdateDistrictCommand : IRequest<MethodResult<UpdateDistrictCommandResponse>>
    {
        public int Id { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string CodeName { get; set; }
        public string DivisionType { get; set; }
        public int? IdProvince { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateDistrictCommandResponse : UpdateDistrictCommand
    {
    }
}
