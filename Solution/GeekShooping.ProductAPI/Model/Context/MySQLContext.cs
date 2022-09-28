using Microsoft.EntityFrameworkCore;

namespace GeekShooping.ProductAPI.Model.Context
{
    public class MySQLContext : DbContext
    {

        public MySQLContext() {}

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "Camiseta Preta",
                Price =  new decimal(69.9),
                Description = "Lorem IPSUM",
                ImageUrl = "",
                CategoryName = "T-Shirt"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 6,
                Name = "Camiseta Branca",
                Price = new decimal(69.9),
                Description = "Lorem IPSUM",
                ImageUrl = "",
                CategoryName = "T-Shirt"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 7,
                Name = "Camiseta Laranja",
                Price = new decimal(69.9),
                Description = "Lorem IPSUM",
                ImageUrl = "",
                CategoryName = "T-Shirt"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 8,
                Name = "Camiseta Azul",
                Price = new decimal(69.9),
                Description = "Lorem IPSUM",
                ImageUrl = "",
                CategoryName = "T-Shirt"

            });

        }
    }
}
