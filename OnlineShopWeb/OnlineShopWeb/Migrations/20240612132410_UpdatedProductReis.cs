using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProductReis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "Category", "Name", "Picture", "Price", "Producer" },
                values: new object[] { 3, "Reis", "reis.jpg", 0.99m, "Bioland" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: -4,
                columns: new[] { "Category", "Name", "Picture", "Price", "Producer" },
                values: new object[] { 2, "Giotto", "giotto.jpg", 2.99m, "Ferrero" });
        }
    }
}
