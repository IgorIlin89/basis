using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "TransactionHistoryToProducts");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPrice",
                table: "TransactionHistory",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "TransactionHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductInCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    TransactionHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInCart_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInCart_TransactionHistory_TransactionHistoryId",
                        column: x => x.TransactionHistoryId,
                        principalTable: "TransactionHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_ProductId",
                table: "TransactionHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCart_ProductId",
                table: "ProductInCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCart_TransactionHistoryId",
                table: "ProductInCart",
                column: "TransactionHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_Product_ProductId",
                table: "TransactionHistory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_Product_ProductId",
                table: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "ProductInCart");

            migrationBuilder.DropIndex(
                name: "IX_TransactionHistory_ProductId",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "TransactionHistory");

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistoryToProducts",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    TransactionHistoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistoryToProducts", x => new { x.ProductsId, x.TransactionHistoriesId });
                    table.ForeignKey(
                        name: "FK_TransactionHistoryToProducts_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionHistoryToProducts_TransactionHistory_TransactionHistoriesId",
                        column: x => x.TransactionHistoriesId,
                        principalTable: "TransactionHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryToProducts_TransactionHistoriesId",
                table: "TransactionHistoryToProducts",
                column: "TransactionHistoriesId");
        }
    }
}
