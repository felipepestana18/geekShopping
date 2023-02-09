using GeekShooping.CartApi.Data.ValueObjects;
using GeekShooping.CartApi.Messages;
using GeekShooping.CartApi.Model.Repository;
using GeekShooping.CartApi.RabbitMQSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : Controller
    {
        private ICartRepository _Cartrepository;
        private ICouponRepository _couponRepository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository repository , IRabbitMQMessageSender rabbitMQMessageSender, ICouponRepository couponRepository)
        {
            _Cartrepository = repository ?? throw new
                ArgumentNullException(nameof(repository));

            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new
               ArgumentNullException(nameof(rabbitMQMessageSender));

            _couponRepository = couponRepository ?? throw new
               ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _Cartrepository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart([FromBody] CartVO vo)
        {
            var cart = await _Cartrepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _Cartrepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _Cartrepository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        // só aplicar quando estive pronto o microserviço de Coupon
        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        {
            var status = await _Cartrepository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
        {
            var status = await _Cartrepository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVo>> Checkout(CheckoutHeaderVo vo)
        {
            // fazendo a requisição do micro serviço de coupon
            string token = Request.Headers["Authorization"];

            var cart = await _Cartrepository.FindCartByUserId(vo.UserId);
            if (cart == null) return NotFound();

            // verificação para ver se o coupon é null
            if (!string.IsNullOrEmpty(vo.CouponCode))
            {
                CouponVO coupon = await _couponRepository.GetCouponByCouponCode(vo.CouponCode, token);
                if(vo.DiscountAmount != coupon.DiscountAmount)
                {
                    // retornando 412 que mudou as codições
                    return StatusCode(412);
                }
            }

            // montado o carrinho 
            vo.cartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            //TASK RabbitMQ  logic comes here 
            // adicionar só quando a configuração do rrabitMq estive pronto;
            _rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");
            return Ok(vo);
        }
    }
}
