﻿using GeekShooping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShooping.CartApi.Model
{
    [Table("product")]
    public class Product 
    {
        // DESSE JEITO NÃO TEM AUTO-ICREMENTO 
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("price")]
        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }

        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; } 

        [Column("category_name")]
        [StringLength(50)]
        public string CategoryName {get; set; }

        [Column("image_url")]
        [StringLength(300)]
        public string ImageUrl { get; set; }

    }
}