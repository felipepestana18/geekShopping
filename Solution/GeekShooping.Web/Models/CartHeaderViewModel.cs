﻿
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace GeekShooping.Web.Models
{

    public class CartHeaderViewModel
    {

        public long Id { get; set; }

        public string UserId { get; set; }

        public string CouponCode { get; set; } = string.Empty;  

        public decimal PurchaseAmount{ get;set;}

        public decimal DiscountTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateTime { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string CardNumber { get; set; }

        public string CVV { get; set; }

        public DateTime ExpiryMothYear { get; set; }

    }
}
