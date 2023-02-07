using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GeekShooping.CouponApi.data.ValueObjects 
{
    public class CouponVO
    {

        public long Id { get; set; }

        public string CouponCode { get; set; }

        public decimal DiscountAmount { get; set; }
    }
}
