

using GeekShooping.OrderApi.Model;

namespace GeekShooping.CartApi.Model.Repository
{
    public interface IOrderRepository
    {

        Task<bool> AddOrder(OrderHeader header);

        Task UpdateOrderPaymentStatus(long orderHeaderId, bool paid);


    }
}
