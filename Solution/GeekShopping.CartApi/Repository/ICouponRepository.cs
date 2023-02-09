

using GeekShooping.CartApi.Data.ValueObjects;

namespace GeekShooping.CartApi.Model.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode, string token);

    }
}
