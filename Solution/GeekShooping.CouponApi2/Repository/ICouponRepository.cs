using GeekShooping.CouponApi.data.ValueObjects;

namespace GeekShooping.CouponApi.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);

    }
}
