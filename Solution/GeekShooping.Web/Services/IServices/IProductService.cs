using GeekShooping.Web.Models;

namespace GeekShooping.Web.Services.IServices
{
    public interface IProductService
    {
        // quando o Identity Server estive pronto coma Token eu passo o pârametro
        Task<IEnumerable<ProductViewModel>> FindAllProducts(string token);

        Task<ProductViewModel> FindProductById(long id, string token);

        Task<ProductViewModel> CreateProduct(ProductViewModel model, string token);

        Task<ProductViewModel> UpdateProduct(ProductViewModel model, string token);

        Task<bool> DeleteProductById(long id, string token);

    }
}
