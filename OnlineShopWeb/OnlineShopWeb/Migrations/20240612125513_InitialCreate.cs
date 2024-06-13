using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Producer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "TransactionHistoryToUsers",
                columns: table => new
                {
                    TransactionHistoriesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistoryToUsers", x => new { x.TransactionHistoriesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TransactionHistoryToUsers_TransactionHistory_TransactionHistoriesId",
                        column: x => x.TransactionHistoriesId,
                        principalTable: "TransactionHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionHistoryToUsers_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "Id", "AmountOfDiscount", "Code", "EndDate", "MaxNumberOfUses", "StartDate", "TypeOfDiscount" },
                values: new object[,]
                {
                    { -5, 75.0, "TestMaxNumberOfUses", new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 0L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { -4, 50.0, "Testing", new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 554L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { -3, 25.0, "Test", new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 670L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 1 },
                    { -2, 22.0, "TestOutOfRange", new DateTimeOffset(new DateTime(2020, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 32L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 2 },
                    { -1, 111.0, "TestInRange", new DateTimeOffset(new DateTime(2026, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 323L, new DateTimeOffset(new DateTime(2022, 1, 1, 12, 12, 12, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), 2 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Category", "Count", "Name", "Picture", "Price", "Producer" },
                values: new object[,]
                {
                    { -4, 2, 0, "Giotto", "giotto.jpg", 2.99m, "Ferrero" },
                    { -3, 2, 0, "Giotto", "giotto.jpg", 2.99m, "Ferrero" },
                    { -2, 4, 0, "Inkpad 4", "inkpad4.jpg", 239.99m, "Pocketbook" },
                    { -1, 1, 0, "Persil", "persil.jpg", 5.99m, "Henkel" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "City", "Country", "EMail", "GivenName", "HouseNumber", "Password", "PostalCode", "Street", "Surname" },
                values: new object[,]
                {
                    { -3, 33, "Berlin", "Germany", "dirk@gmail.com", "Dirk", 232, "123456", 25014, "Berliner Straße", "Es" },
                    { -2, 38, "Harburg", "Germany", "yury@gmail.com", "Yury", 22, "123456", 22041, "Harburger Chaussee", "Spi" },
                    { -1, 34, "Hamburg", "Germany", "igor@gmail.com", "Igor", 154, "123456", 22526, "Berner Chaussee", "Il" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryToCoupons_TransactionHistoriesId",
                table: "TransactionHistoryToCoupons",
                column: "TransactionHistoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryToProducts_TransactionHistoriesId",
                table: "TransactionHistoryToProducts",
                column: "TransactionHistoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryToUsers_UsersId",
                table: "TransactionHistoryToUsers",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCart");

            migrationBuilder.DropTable(
                name: "TransactionHistoryToCoupons");

            migrationBuilder.DropTable(
                name: "TransactionHistoryToProducts");

            migrationBuilder.DropTable(
                name: "TransactionHistoryToUsers");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
