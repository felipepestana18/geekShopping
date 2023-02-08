
namespace GeekShooping.Web.Models
{

    public class CartHeaderViewModel
    {

        public long Id { get; set; }

        public string UserId { get; set; }

        public string CouponCode { get; set; } = "1";

        public decimal PurchaseAmount{ get;set;}

    }
}
