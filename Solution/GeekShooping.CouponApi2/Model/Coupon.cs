using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using GeekShooping.CouponApi.Model.Base;

namespace GeekShooping.CouponApi.Model
{

    [Table("coupon")]
    public class Coupon : BaseEntity
    {
        [Column("coupon_code")]
        [Required]
        [StringLength(30)]
        public string CouponCode { get; set; }

        [Column("Discount_amount")]
        [Required]
        public decimal DiscountAmount { get; set; }
    }

}
