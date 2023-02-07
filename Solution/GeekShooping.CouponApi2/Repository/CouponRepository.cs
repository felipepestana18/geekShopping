using AutoMapper;
using GeekShooping.CouponApi.data.ValueObjects;
using GeekShooping.CouponApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.CouponApi.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;



        public CouponRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);

            return _mapper.Map<CouponVO>(coupon);
        }
    }
}
