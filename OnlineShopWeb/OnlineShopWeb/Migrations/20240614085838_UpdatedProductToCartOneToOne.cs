using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProductToCartOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductInCart_ProductId",
                table: "ProductInCart");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCart_ProductId",
                table: "ProductInCart",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductInCart_ProductId",
                table: "ProductInCart");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCart_ProductId",
                table: "ProductInCart",
                column: "ProductId");
        }
    }
}
