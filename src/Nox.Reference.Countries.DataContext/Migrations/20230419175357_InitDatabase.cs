using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nox.Reference.Country.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyUsage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyUsage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MajorCurrencyUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorCurrencyUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinorCurrencyUnit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    MajorValue = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorCurrencyUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyFrequentUsage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyUsageId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyFrequentUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyFrequentUsage_CurrencyUsage_CurrencyUsageId",
                        column: x => x.CurrencyUsageId,
                        principalTable: "CurrencyUsage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRareUsage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyUsageId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRareUsage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRareUsage_CurrencyUsage_CurrencyUsageId",
                        column: x => x.CurrencyUsageId,
                        principalTable: "CurrencyUsage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    MajorUnitId = table.Column<int>(type: "INTEGER", nullable: true),
                    MinorUnitId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_Currency_MajorCurrencyUnit_MajorUnitId",
                        column: x => x.MajorUnitId,
                        principalTable: "MajorCurrencyUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Currency_MinorCurrencyUnit_MinorUnitId",
                        column: x => x.MinorUnitId,
                        principalTable: "MinorCurrencyUnit",
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
                name: "IX_Currency_MajorUnitId",
                table: "Currency",
                column: "MajorUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_MinorUnitId",
                table: "Currency",
                column: "MinorUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyFrequentUsage_CurrencyUsageId",
                table: "CurrencyFrequentUsage",
                column: "CurrencyUsageId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRareUsage_CurrencyUsageId",
                table: "CurrencyRareUsage",
                column: "CurrencyUsageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "CurrencyFrequentUsage");

            migrationBuilder.DropTable(
                name: "CurrencyRareUsage");

            migrationBuilder.DropTable(
                name: "MajorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "MinorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "CurrencyUsage");
        }
    }
}
