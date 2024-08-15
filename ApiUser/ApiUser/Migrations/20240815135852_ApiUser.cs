using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiUser.Migrations
{
    /// <inheritdoc />
    public partial class ApiUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "City", "Country", "EMail", "GivenName", "HouseNumber", "Password", "PostalCode", "Street", "Surname" },
                values: new object[,]
                {
                    { 1, 34, "Hamburg", "Germany", "igor@gmail.com", "Igor", 154, "123456", 22526, "Berner Chaussee", "Il" },
                    { 2, 38, "Harburg", "Germany", "yury@gmail.com", "Yury", 22, "123456", 22041, "Harburger Chaussee", "Spi" },
                    { 3, 33, "Berlin", "Germany", "dirk@gmail.com", "Dirk", 232, "123456", 25014, "Berliner Straße", "Es" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
