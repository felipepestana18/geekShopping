﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.ProductAPI.Model.Base
{
    public class BaseEntity
    {

        [Key]
        [Column("Id")]
        public long Id { get; set; }
    }
}
