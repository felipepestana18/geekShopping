using GeekShooping.CartApi.Model.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.CartApi.Model
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {

        // muitos para muitos
        // sempre é necessário criar o Id e o Tipo nome da Classe para enitty framwork
        public long CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }


        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Column("count")]
        public long Count { get; set; } 
    }
}
