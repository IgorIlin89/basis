using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiOnlineShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class CouponPlusTransactionhistoryCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "Surname");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HouseNumber",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountOfDiscount = table.Column<double>(type: "float", nullable: false),
                    TypeOfDiscount = table.Column<int>(type: "int", nullable: false),
                    MaxNumberOfUses = table.Column<long>(type: "bigint", nullable: true),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FinalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionHistory_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductInCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "TransactionHistoryToCoupons",
                columns: table => new
                {
                    CouponsId = table.Column<int>(type: "int", nullable: false),
                    TransactionHistoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistoryToCoupons", x => new { x.CouponsId, x.TransactionHistoriesId });
                    table.ForeignKey(
                        name: "FK_TransactionHistoryToCoupons_Coupon_CouponsId",
                        column: x => x.CouponsId,
                        principalTable: "Coupon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionHistoryToCoupons_TransactionHistory_TransactionHistoriesId",
                        column: x => x.TransactionHistoriesId,
                        principalTable: "TransactionHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "AmountOfDiscount", "Code", "EndDate", "MaxNumberOfUses", "StartDate", "TypeOfDiscount" },
                values: new object[,]
                {
                    { 1, 111.0, "TestInRange", new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 323L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 2 },
                    { 2, 22.0, "TestOutOfRange", new DateTimeOffset(new DateTime(2020, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 32L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 2 },
                    { 3, 25.0, "Test", new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 670L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 4, 50.0, "Testing", new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 554L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { 5, 75.0, "TestMaxNumberOfUses", new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 1 }
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "City", "Country", "GivenName", "HouseNumber", "PostalCode", "Street", "Surname" },
                values: new object[] { 34, "Hamburg", "Germany", "Igor", 154, 22526, "Berner Chaussee", "Il" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Age", "City", "Country", "GivenName", "HouseNumber", "PostalCode", "Street", "Surname" },
                values: new object[] { 38, "Harburg", "Germany", "Yury", 22, 22041, "Harburger Chaussee", "Spi" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "City", "Country", "EMail", "GivenName", "HouseNumber", "Password", "PostalCode", "Street", "Surname" },
                values: new object[] { 3, 33, "Berlin", "Germany", "dirk@gmail.com", "Dirk", 232, "123456", 25014, "Berliner Straße", "Es" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCart_ProductId",
                table: "ProductInCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInCart_TransactionHistoryId",
                table: "ProductInCart",
                column: "TransactionHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_ProductId",
                table: "TransactionHistory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_UserId",
                table: "TransactionHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryToCoupons_TransactionHistoriesId",
                table: "TransactionHistoryToCoupons",
                column: "TransactionHistoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInCart");

            migrationBuilder.DropTable(
                name: "TransactionHistoryToCoupons");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Age",
                table: "User");

            migrationBuilder.DropColumn(
                name: "City",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Igor Il");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Yury Spi");
        }
    }
}
