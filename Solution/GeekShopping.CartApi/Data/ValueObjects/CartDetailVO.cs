
namespace GeekShooping.CartApi.Data.ValueObjects
{

    public class CartDetailVO
    {

        // muitos para muitos
        // sempre é necessário criar o Id e o Tipo nome da Classe para enitty framwork
        public long CartHeaderId { get; set; }
        public CartHeaderVO CartHeader { get; set; }

        public long ProductId { get; set; }
        public ProductVO Product { get; set; }

        public long Count { get; set; } 
    }
}
