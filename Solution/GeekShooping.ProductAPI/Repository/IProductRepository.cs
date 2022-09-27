using GeekShooping.ProductAPI.Data.ValueObjects;

namespace GeekShooping.ProductAPI.Repository
{
    public interface IProductRepository
    {
       //para retorna uma lista de produtos
        Task<IEnumerable<ProductVO>> FindAll();

        Task<ProductVO> FindById(long id);

        Task<ProductVO> Create(ProductVO vo);

        Task<ProductVO> Update(ProductVO vo);

        Task<bool> Delete(long id);

    }
}
