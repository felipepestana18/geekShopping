
namespace GeekShooping.OrderApi.Messages
{

    public class CartDetailVO
    {

        // muitos para muitos
        // sempre é necessário criar o Id e o Tipo nome da Classe para enitty framwork
        public long Id { get; set; }

        public long CartHeaderId { get; set; }


        public long ProductId { get; set; }
        public virtual ProductVO Product { get; set; }

        public long Count { get; set; } 
    }
}
