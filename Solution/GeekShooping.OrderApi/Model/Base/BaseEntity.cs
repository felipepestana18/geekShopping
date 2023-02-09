using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.OrderApi.Model.Base
{
    public class BaseEntity
    {

        [Key]
        [Column("Id")]
        public long Id { get; set; }
    }
}
