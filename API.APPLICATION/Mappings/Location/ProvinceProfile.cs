using API.APPLICATION.ViewModels.Project;
using API.DOMAIN.DTOs.Project;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.Mappings.Location
{
    public class ProvinceProfile :Profile
    {
        public ProvinceProfile()
        {
            CreateMap<ProjectDTO, ProjectResponseViewModel>();
            CreateMap<ProjectRequestViewModel,  DanhMucFilterParam> ();
        }
    }
}
