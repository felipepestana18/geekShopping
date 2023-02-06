using GeekShooping.CartApi.Data.ValueObjects;
using GeekShooping.CartApi.Model.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : Controller
    {
        private ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));

        }

        [HttpGet("find-cart/{{id}}")]
        // [Authorize] permitindo que o usuário consiga ver o find all
        public async Task<ActionResult<IEnumerable<CartVO>>> FindById(string id)
        {
            var cart = await _repository.FindCartByUserId(id);
            return Ok(cart);

        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<IEnumerable<CartVO>>> AddCart(CartVO vo)
        {
            var cart = await _repository.SaveOrUpdateCart(vo);
            if (cart != null) return NotFound();
            return Ok(cart);

        }
        [HttpPut("update-cart")]
        public async Task<ActionResult<IEnumerable<CartVO>>> UpdateCART(CartVO vo)
        {
            var cart = await _repository.SaveOrUpdateCart(vo);
            if (cart != null) return NotFound();
            return Ok(cart);

        }
        [HttpDelete("remove-cart/{{id}}")]

        public async Task<ActionResult<IEnumerable<CartVO>>> RemoveCARD(int id)
        {
            bool status = await _repository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);

        }

    }
}
