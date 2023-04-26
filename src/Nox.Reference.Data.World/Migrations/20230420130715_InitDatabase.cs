using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nox.Reference.Data.World.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Culture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FormalName = table.Column<string>(type: "TEXT", nullable: false),
                    NativeName = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: true),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayNameWithDialect = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterOrientation = table.Column<string>(type: "TEXT", nullable: false),
                    LineOrientation = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageIso_639_2t = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Culture", x => x.Id);
                });

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
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false)
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
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    MajorValue = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorCurrencyUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateFormat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AmPmStrings = table.Column<string>(type: "TEXT", nullable: false),
                    Eras = table.Column<string>(type: "TEXT", nullable: false),
                    EraNames = table.Column<string>(type: "TEXT", nullable: false),
                    Months = table.Column<string>(type: "TEXT", nullable: false),
                    ShortMonths = table.Column<string>(type: "TEXT", nullable: false),
                    ShortWeekdays = table.Column<string>(type: "TEXT", nullable: false),
                    Weekdays = table.Column<string>(type: "TEXT", nullable: false),
                    Date_3 = table.Column<string>(type: "TEXT", nullable: false),
                    Date_2 = table.Column<string>(type: "TEXT", nullable: false),
                    Date_1 = table.Column<string>(type: "TEXT", nullable: false),
                    Date_0 = table.Column<string>(type: "TEXT", nullable: false),
                    CultureId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateFormat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DateFormat_Culture_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Culture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumberFormat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrencySymbol = table.Column<string>(type: "TEXT", nullable: false),
                    DecimalSeparator = table.Column<string>(type: "TEXT", nullable: false),
                    Digit = table.Column<string>(type: "TEXT", nullable: false),
                    ExponentSeparator = table.Column<string>(type: "TEXT", nullable: false),
                    GroupingSeparator = table.Column<string>(type: "TEXT", nullable: false),
                    Infinity = table.Column<string>(type: "TEXT", nullable: false),
                    InternationalCurrencySymbol = table.Column<string>(type: "TEXT", nullable: false),
                    MinusSign = table.Column<string>(type: "TEXT", nullable: false),
                    MonetaryDecimalSeparator = table.Column<string>(type: "TEXT", nullable: false),
                    NotANumberSymbol = table.Column<string>(type: "TEXT", nullable: false),
                    PadEscape = table.Column<string>(type: "TEXT", nullable: false),
                    PatternSeparator = table.Column<string>(type: "TEXT", nullable: false),
                    Percent = table.Column<string>(type: "TEXT", nullable: false),
                    PerMill = table.Column<string>(type: "TEXT", nullable: false),
                    PlusSign = table.Column<string>(type: "TEXT", nullable: false),
                    SignificantDigit = table.Column<string>(type: "TEXT", nullable: false),
                    ZeroDigit = table.Column<string>(type: "TEXT", nullable: false),
                    CultureId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberFormat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumberFormat_Culture_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Culture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    MajorUnitId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinorUnitId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_DateFormat_CultureId",
                table: "DateFormat",
                column: "CultureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NumberFormat_CultureId",
                table: "NumberFormat",
                column: "CultureId",
                unique: true);
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
                name: "DateFormat");

            migrationBuilder.DropTable(
                name: "NumberFormat");

            migrationBuilder.DropTable(
                name: "MajorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "MinorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "CurrencyUsage");

            migrationBuilder.DropTable(
                name: "Culture");
        }
    }
}
