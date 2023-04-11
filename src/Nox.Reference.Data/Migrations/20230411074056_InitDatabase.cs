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
                name: "CurrencyUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MajorName = table.Column<string>(type: "TEXT", nullable: true),
                    MinorName = table.Column<string>(type: "TEXT", nullable: true),
                    MajorSymbol = table.Column<string>(type: "TEXT", nullable: true),
                    MinorSymbol = table.Column<string>(type: "TEXT", nullable: true),
                    MajorValue = table.Column<string>(type: "TEXT", nullable: true),
                    MinorValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyUsage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Frequent = table.Column<string>(type: "TEXT", nullable: true),
                    Rare = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyUsage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IEEERegistry = table.Column<string>(type: "TEXT", nullable: false),
                    MacPrefix = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationName = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationAddress = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsoCode = table.Column<string>(type: "TEXT", nullable: false),
                    IsoNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    ThousandsSeparator = table.Column<string>(type: "TEXT", nullable: false),
                    DecimalSeparator = table.Column<string>(type: "TEXT", nullable: false),
                    SymbolOnLeft = table.Column<bool>(type: "INTEGER", nullable: false),
                    SpaceBetweenAmountAndSymbol = table.Column<bool>(type: "INTEGER", nullable: false),
                    DecimalDigits = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BanknotesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoinsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currency_CurrencyUnit_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "CurrencyUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Currency_CurrencyUsage_BanknotesId",
                        column: x => x.BanknotesId,
                        principalTable: "CurrencyUsage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Currency_CurrencyUsage_CoinsId",
                        column: x => x.CoinsId,
                        principalTable: "CurrencyUsage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currency_BanknotesId",
                table: "Currency",
                column: "BanknotesId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_CoinsId",
                table: "Currency",
                column: "CoinsId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_UnitsId",
                table: "Currency",
                column: "UnitsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "MacAddress");

            migrationBuilder.DropTable(
                name: "CurrencyUnit");

            migrationBuilder.DropTable(
                name: "CurrencyUsage");
        }
    }
}
