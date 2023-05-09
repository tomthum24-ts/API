using BaseCommon.Common.MethodResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Commands.Location.District
{
    public class CreateDistrictCommand : IRequest<MethodResult<CreateDistrictCommandResponse>>
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string CodeName { get; set; }
        public string DivisionType { get; set; }
        public int IdProvince { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }
    }
    public class CreateDistrictCommandResponse : CreateDistrictCommand
    {

    }
}
