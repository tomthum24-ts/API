using API.APPLICATION.Parameters.Menu;
using API.APPLICATION.ViewModels;
using AutoMapper;

namespace API.Mapper.Menu
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<MenuRequestViewModel, MenuFilterParam>();
          
        }
    }
}
