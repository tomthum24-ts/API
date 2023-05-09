
using BaseCommon.Common.MethodResult;
using MediatR;

namespace API.APPLICATION.Commands.Location.Village
{
    public class CreateVillageCommand : IRequest<MethodResult<CreateVillageCommandResponse>>
    {
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public string CodeName { get; set; }
        public string DivisionType { get; set; }
        public int? IdDistrict { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class CreateVillageCommandResponse : CreateVillageCommand
    {

    }
}
