using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiOnlineShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddedProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Producer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Category", "Name", "Picture", "Price", "Producer" },
                values: new object[,]
                {
                    { 1, 1, "Persil", "persil.jpg", 5.99m, "Henkel" },
                    { 2, 4, "Inkpad 4", "inkpad4.jpg", 239.99m, "Pocketbook" },
                    { 3, 2, "Giotto", "giotto.jpg", 2.99m, "Ferrero" },
                    { 4, 3, "Reis", "reis.jpg", 0.99m, "Bioland" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "EMail", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "igor@gmail.com", "Igor Il", "123456" },
                    { 2, "yury@gmail.com", "Yury Spi", "123456" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "EMail", "Name", "Password" },
                values: new object[,]
                {
                    { -2, "yury@gmail.com", "Yury Spi", "123456" },
                    { -1, "igor@gmail.com", "Igor Il", "123456" }
                });
        }
    }
}
