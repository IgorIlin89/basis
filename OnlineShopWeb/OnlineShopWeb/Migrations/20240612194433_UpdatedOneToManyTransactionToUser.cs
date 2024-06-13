using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopWeb.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOneToManyTransactionToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionHistoryToUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TransactionHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_UserId",
                table: "TransactionHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistory_User_UserId",
                table: "TransactionHistory",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistory_User_UserId",
                table: "TransactionHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionHistory_UserId",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TransactionHistory");

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

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistoryToUsers_UsersId",
                table: "TransactionHistoryToUsers",
                column: "UsersId");
        }
    }
}
