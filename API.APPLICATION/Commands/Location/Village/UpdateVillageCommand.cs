using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Location.Village
{
    public class UpdateVillageCommand : IRequest<MethodResult<UpdateVillageCommandResponse>>
    {
        public int Id { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public string CodeName { get; set; }
        public string DivisionType { get; set; }
        public int? IdDistrict { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class UpdateVillageCommandResponse : UpdateVillageCommand
    {
    }
}
