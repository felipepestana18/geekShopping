
namespace GeekShooping.Web.Models
{

    public class CartDetailViewModel
    {

        // muitos para muitos
        // sempre é necessário criar o Id e o Tipo nome da Classe para enitty framwork
        public long CartHeaderId { get; set; }
        public CartHeaderViewModel CartHeader { get; set; }

        public long ProductId { get; set; }
        public ProductViewModel Product { get; set; }

        public long Count { get; set; } 
    }
}
