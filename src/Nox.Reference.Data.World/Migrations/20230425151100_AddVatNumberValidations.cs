using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nox.Reference.Data.World.Migrations
{
    /// <inheritdoc />
    public partial class AddVatNumberValidations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VatNumberDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    LocalName = table.Column<string>(type: "TEXT", nullable: false),
                    VerificationApi = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatNumberDefinition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VatNumberValidationRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    TranslationId = table.Column<string>(type: "TEXT", nullable: false),
                    Regex = table.Column<string>(type: "TEXT", nullable: false),
                    ValidationFormatDescription = table.Column<string>(type: "TEXT", nullable: false),
                    InputMask = table.Column<string>(type: "TEXT", nullable: false),
                    MinimumLength = table.Column<int>(type: "INTEGER", nullable: false),
                    MaximumLength = table.Column<int>(type: "INTEGER", nullable: false),
                    Checksum_Algorithm = table.Column<int>(type: "INTEGER", nullable: true),
                    Checksum_ChecksumDigit = table.Column<string>(type: "TEXT", nullable: true),
                    Checksum_Modulus = table.Column<int>(type: "INTEGER", nullable: true),
                    Checksum_Weights = table.Column<string>(type: "TEXT", nullable: true),
                    VatNumberDefinitionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatNumberValidationRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VatNumberValidationRule_VatNumberDefinition_VatNumberDefinitionId",
                        column: x => x.VatNumberDefinitionId,
                        principalTable: "VatNumberDefinition",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VatNumberValidationRule_VatNumberDefinitionId",
                table: "VatNumberValidationRule",
                column: "VatNumberDefinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VatNumberValidationRule");

            migrationBuilder.DropTable(
                name: "VatNumberDefinition");
        }
    }
}
