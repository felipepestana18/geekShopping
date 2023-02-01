using AutoMapper;
using GeekShooping.CartApi.Data.ValueObjects;
using GeekShooping.CartApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShooping.CartApi.Model.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;



        public CartRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<bool> ApplyCoupon(string userId, long couponCode)
        {

            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            // verificando se tem userId
            var cartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
            
            if (cartHeader != null)
            {
                // pesquisando CartDetails primeiro para se existe para remove, e removendo da FOREIN KEY
                _context.CartDetails.RemoveRange(
                    _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));

                // removendo cartHeader da tabela principal
                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();  
                return true;
            }
            return false;
      
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId),
            };
            // vai returna detalhe do carrinho e produtos relacionado.
            cart.CartDetails = _context.CartDetails.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(c => c.Product);
           return _mapper.Map<CartVO>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                // removendo o detalhe do carrinho
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);
                int total = _context.CartDetails.Where(c => c.CartHeaderId == cartDetailsId).Count();

                _context.CartDetails.Remove(cartDetail);
                if(total == 1)
                {
                    // removendo header do carrinho 
                    var cartGeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartDetailsId);
                    _context.CartHeaders.Remove(cartGeaderToRemove);
                    _context.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            // transformado em VO
            Cart cart = _mapper.Map<Cart>(vo);

            // verificando se existe produto criado
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == vo.CartDetails.FirstOrDefault().ProductId);
            if (product == null)
            { // adicionando um produto
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            // verificando se existe cart header
            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                         c => c.UserId == cart.CartHeader.UserId);

            if (cartHeader == null)
            {
                // adicionado CartHeader and CarDetails
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                // já foi salvo lá em cima por isso que não é necessário salvar duas vezes, se não vai dar conflito
                cart.CartDetails.FirstOrDefault().Product = null;

                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                // verificando se tem cart detail
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == vo.CartDetails.FirstOrDefault().ProductId &&
                p.CartHeaderId == cartHeader.Id);

                if (cartDetail == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                    // já foi salvo lá em cima por isso que não é necessário salvar duas vezes, se não vai dar conflito
                    cart.CartDetails.FirstOrDefault().Product = null;

                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                    // criando cartDetails
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    await _context.SaveChangesAsync();

                    // atualizando o product e carDetais
                }
            }

            // mapeando e retornando para client o cart
            return _mapper.Map<CartVO>(cart);
        }
    }
}
