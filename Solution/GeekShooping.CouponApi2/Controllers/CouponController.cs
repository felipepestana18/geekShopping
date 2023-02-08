using GeekShooping.CouponApi.data.ValueObjects;
using GeekShooping.CouponApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.CouponApi2.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponController : Controller
    {
        private ICouponRepository _repository;

        public CouponController(ICouponRepository repository)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));

        }

        [HttpGet("{couponCode}")]
        public async Task<ActionResult<CouponVO>> GetCouponByCouponCode(string coupon)
        {
            var result = await _repository.GetCouponByCouponCode(coupon);

            if (result == null)
                return NotFound();
            return Ok(result);

        }



    }
}