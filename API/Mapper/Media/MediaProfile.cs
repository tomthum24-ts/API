using API.APPLICATION.ViewModels.Media;
using API.DOMAIN;
using AutoMapper;

namespace API.Mapper.Media
{
    public class MediaProfile : Profile
    {
        public MediaProfile()
        {
            CreateMap<AttachmentFile, MediaResponse>();
        }
    }
}
