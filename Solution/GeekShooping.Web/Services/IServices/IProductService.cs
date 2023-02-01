using GeekShooping.Web.Models;

namespace GeekShooping.Web.Services.IServices
{
    public interface IProductService
    {
        // quando o Identity Server estive pronto coma Token eu passo o pârametro
        Task<IEnumerable<ProductModel>> FindAllProducts(string token);

        Task<ProductModel> FindProductById(long id, string token);

        Task<ProductModel> CreateProduct(ProductModel model, string token);

        Task<ProductModel> UpdateProduct(ProductModel model, string token);

        Task<bool> DeleteProductById(long id, string token);

    }
}
