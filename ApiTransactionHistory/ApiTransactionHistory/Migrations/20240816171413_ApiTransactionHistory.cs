using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTransactionHistory.Migrations
{
    /// <inheritdoc />
    public partial class ApiTransactionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductInCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PricePerProduct = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TransactionHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInCartDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PricePerProduct = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInCartDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionHistoryToCouponsId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistoryToCoupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionHistoryId = table.Column<int>(type: "int", nullable: false),
                    TransactionHistoryId1 = table.Column<int>(type: "int", nullable: false),
                    CouponsId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistoryToCoupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionHistoryToCoupons_TransactionHistory_TransactionHistoryId1",
                        column: x => x.TransactionHistoryId1,
                        principalTable: "TransactionHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCartDto_TransactionHistoryId",
                table: "ProductInCartDto",
                column: "TransactionHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_TransactionHistoryToCouponsId",
                table: "TransactionHistory",
                column: "TransactionHistoryToCouponsId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryToCoupons_TransactionHistoryId1",
                table: "TransactionHistoryToCoupons",
                column: "TransactionHistoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInCartDto_TransactionHistory_TransactionHistoryId",
                table: "ProductInCartDto",
                column: "TransactionHistoryId",
                principalTable: "TransactionHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_TransactionHistoryToCoupons_TransactionHistoryToCouponsId",
                table: "TransactionHistory",
                column: "TransactionHistoryToCouponsId",
                principalTable: "TransactionHistoryToCoupons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistoryToCoupons_TransactionHistory_TransactionHistoryId1",
                table: "TransactionHistoryToCoupons");

            migrationBuilder.DropTable(
                name: "ProductInCart");

            migrationBuilder.DropTable(
                name: "ProductInCartDto");

            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "TransactionHistoryToCoupons");
        }
    }
}
