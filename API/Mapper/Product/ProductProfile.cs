using API.APPLICATION.ViewModels.Unit;
using API.APPLICATION;
using API.DOMAIN.DTOs.Unit;
using AutoMapper;
using API.APPLICATION.ViewModels.Product;
using API.DOMAIN.DTOs.Product;

namespace API.Mapper.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {

            CreateMap<ProductRequestViewModel, DanhMucFilterParam>();
            CreateMap<ProductDTO, ProductResponseViewModel>();

        }
    }
}
