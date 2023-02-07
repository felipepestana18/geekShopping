using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShooping.CouponApi.Migrations
{
    public partial class DiscountAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "coupon",
                columns: new[] { "Id", "coupon_code", "Discount_amount" },
                values: new object[] { 1L, "ERUDIO_2022_10", 10m });

            migrationBuilder.InsertData(
                table: "coupon",
                columns: new[] { "Id", "coupon_code", "Discount_amount" },
                values: new object[] { 2L, "ERUDIO_2022_15", 15m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "coupon",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "coupon",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
