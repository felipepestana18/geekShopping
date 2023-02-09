

using GeekShooping.OrderApi.Model.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.OrderApi.Model
{
    [Table("order_detail")]
    public class OrderDetail : BaseEntity
    {

        // muitos para muitos
        // sempre é necessário criar o Id e o Tipo nome da Classe para enitty framwork
        public long OrderHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        [Column("ProductId")]
        public long ProductId { get; set; }

        [Column("count")]
        public long Count { get; set; }

        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("price")]
        public decimal Price { get; set; }




    }
}
