using GeekShooping.CartApi.Data.ValueObjects;

namespace GeekShooping.CartApi.Model.Repository
{
    public interface ICartRepository
    {

        Task<CartVO> FindCartByUserId(string userId);

        Task<CartVO> SaveOrUpdateCart(CartVO cart);

        Task<bool> RemoveFromCart(long cartDetailsId);

        // só fazer quando o microserviço de Coupon estive pronto.
        Task<bool> ApplyCoupon(string userId, string couponCode);

        Task<bool> RemoveCoupon(string userId);

        Task<bool> ClearCart(string userId);
    }
}
