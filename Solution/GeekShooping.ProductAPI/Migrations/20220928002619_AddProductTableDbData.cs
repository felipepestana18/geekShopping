using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShooping.ProductAPI.Migrations
{
    public partial class AddProductTableDbData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "Id", "category_name", "description", "image_url", "name", "price" },
                values: new object[,]
                {
                    { 5L, "T-Shirt", "Lorem IPSUM", "", "Camiseta Preta", 69.9m },
                    { 6L, "T-Shirt", "Lorem IPSUM", "", "Camiseta Branca", 69.9m },
                    { 7L, "T-Shirt", "Lorem IPSUM", "", "Camiseta Laranja", 69.9m },
                    { 8L, "T-Shirt", "Lorem IPSUM", "", "Camiseta Azul", 69.9m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "Id",
                keyValue: 8L);
        }
    }
}
