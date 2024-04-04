using API.APPLICATION.Parameters.Menu;
using API.APPLICATION.ViewModels;
using AutoMapper;

namespace API.Mapper.Notification
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<MenuRequestViewModel, MenuFilterParam>();

        }

    }
}
