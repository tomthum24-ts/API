using API.DOMAIN.DTOs.Product;
using API.INFRASTRUCTURE.DataConnect;

namespace API.APPLICATION.Queries.Product
{
    public interface IProductServices :IDanhMucQueries<ProductDTO>
    {
    }
    public class ProductServices : DanhMucQueries<ProductDTO>, IProductServices
    {
        public ProductServices(DapperContext connectionFactory) : base(connectionFactory)
        {
        }
    }
}