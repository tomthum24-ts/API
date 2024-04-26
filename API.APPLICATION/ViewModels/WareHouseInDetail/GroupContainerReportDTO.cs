using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.WareHouseInDetail
{
    public class GroupContainerReportDTO : WareHouseInDetailResponseDTO
    {
        public List<ContainerReportDTO> ContainerNumbers { get; set; }
    }
    public class ContainerReportDTO
    {
        public string SoContainer { get; set; }
        public int? STT { get; set; }
    }
}
