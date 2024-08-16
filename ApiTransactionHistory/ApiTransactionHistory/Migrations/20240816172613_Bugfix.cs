using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTransactionHistory.Migrations
{
    /// <inheritdoc />
    public partial class Bugfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInCartDto");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCart_TransactionHistoryId",
                table: "ProductInCart",
                column: "TransactionHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInCart_TransactionHistory_TransactionHistoryId",
                table: "ProductInCart",
                column: "TransactionHistoryId",
                principalTable: "TransactionHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInCart_TransactionHistory_TransactionHistoryId",
                table: "ProductInCart");

            migrationBuilder.DropIndex(
                name: "IX_ProductInCart_TransactionHistoryId",
                table: "ProductInCart");

            migrationBuilder.CreateTable(
                name: "ProductInCartDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    PricePerProduct = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TransactionHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInCartDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInCartDto_TransactionHistory_TransactionHistoryId",
                        column: x => x.TransactionHistoryId,
                        principalTable: "TransactionHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCartDto_TransactionHistoryId",
                table: "ProductInCartDto",
                column: "TransactionHistoryId");
        }
    }
}
