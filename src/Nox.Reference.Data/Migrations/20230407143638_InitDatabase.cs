using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nox.Reference.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MacAddresses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    MacPrefix = table.Column<string>(type: "TEXT", nullable: false),
                    IEEERegistry = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    OrganizationName = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationAddress = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacAddresses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MacAddresses");
        }
    }
}
