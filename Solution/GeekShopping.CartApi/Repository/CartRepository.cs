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
            if (cart.CartHeader != null)
            {
                cart.CartDetails = _context.CartDetails.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(c => c.Product);
                return _mapper.Map<CartVO>(cart);
            }
            return _mapper.Map<CartVO>(null);
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
                {
                    int total = _context.CartDetails.Where(c => c.Id == cartDetailsId).Count();

                    _context.CartDetails.Remove(cartDetail);

                    if (total == 1)
                    {
                        // removendo header do carrinho 
                        var cartGeaderToRemove = await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                        _context.CartHeaders.Remove(cartGeaderToRemove);
                      
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            Cart cart = _mapper.Map<Cart>(vo);
            //Checks if the product is already saved in the database if it does not exist then save
            var product = await _context.Products.FirstOrDefaultAsync(
                p => p.Id == vo.CartDetails.FirstOrDefault().ProductId);

            if (product == null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            //Check if CartHeader is null

            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                c => c.UserId == cart.CartHeader.UserId);

            if (cartHeader == null)
            {
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                //If CartHeader is not null
                //Check if CartDetails has same product
                var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == cartHeader.Id);

                if (cartDetail == null)
                {
                    //Create CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //Update product count and CartDetails
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);
        }
    }
}
