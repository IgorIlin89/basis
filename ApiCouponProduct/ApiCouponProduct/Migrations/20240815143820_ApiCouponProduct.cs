using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiCouponProduct.Migrations
{
    /// <inheritdoc />
    public partial class ApiCouponProduct : Migration
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
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
