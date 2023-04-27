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
                name: "CountryHoliday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    CountryName = table.Column<string>(type: "TEXT", nullable: true),
                    DayOff = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryHoliday", x => x.Id);
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
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Iso_639_1 = table.Column<string>(type: "TEXT", nullable: true),
                    Iso_639_2b = table.Column<string>(type: "TEXT", nullable: true),
                    Iso_639_2t = table.Column<string>(type: "TEXT", nullable: true),
                    Iso_639_3 = table.Column<string>(type: "TEXT", nullable: false),
                    Common = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Scope = table.Column<int>(type: "INTEGER", nullable: false),
                    WikiUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
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
                name: "StateHoliday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    StateName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryHolidayId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateHoliday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateHoliday_CountryHoliday_CountryHolidayId",
                        column: x => x.CountryHolidayId,
                        principalTable: "CountryHoliday",
                        principalColumn: "Id");
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
                name: "LanguageTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Translation = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageTranslation_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "RegionHoliday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Region = table.Column<string>(type: "TEXT", nullable: false),
                    RegionName = table.Column<string>(type: "TEXT", nullable: false),
                    StateHolidayId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionHoliday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionHoliday_StateHoliday_StateHolidayId",
                        column: x => x.StateHolidayId,
                        principalTable: "StateHoliday",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HolidayData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    CountryHolidayId = table.Column<int>(type: "INTEGER", nullable: true),
                    RegionHolidayId = table.Column<int>(type: "INTEGER", nullable: true),
                    StateHolidayId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HolidayData_CountryHoliday_CountryHolidayId",
                        column: x => x.CountryHolidayId,
                        principalTable: "CountryHoliday",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HolidayData_RegionHoliday_RegionHolidayId",
                        column: x => x.RegionHolidayId,
                        principalTable: "RegionHoliday",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HolidayData_StateHoliday_StateHolidayId",
                        column: x => x.StateHolidayId,
                        principalTable: "StateHoliday",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LocalHolidayName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Language = table.Column<string>(type: "TEXT", nullable: true),
                    HolidayDataId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalHolidayName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalHolidayName_HolidayData_HolidayDataId",
                        column: x => x.HolidayDataId,
                        principalTable: "HolidayData",
                        principalColumn: "Id");
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
                name: "IX_HolidayData_CountryHolidayId",
                table: "HolidayData",
                column: "CountryHolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayData_RegionHolidayId",
                table: "HolidayData",
                column: "RegionHolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayData_StateHolidayId",
                table: "HolidayData",
                column: "StateHolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTranslation_LanguageId",
                table: "LanguageTranslation",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalHolidayName_HolidayDataId",
                table: "LocalHolidayName",
                column: "HolidayDataId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionHoliday_StateHolidayId",
                table: "RegionHoliday",
                column: "StateHolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_StateHoliday_CountryHolidayId",
                table: "StateHoliday",
                column: "CountryHolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_VatNumberValidationRule_VatNumberDefinitionId",
                table: "VatNumberValidationRule",
                column: "VatNumberDefinitionId");
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
                name: "LanguageTranslation");

            migrationBuilder.DropTable(
                name: "LocalHolidayName");

            migrationBuilder.DropTable(
                name: "VatNumberValidationRule");

            migrationBuilder.DropTable(
                name: "MajorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "MinorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "CurrencyUsage");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "HolidayData");

            migrationBuilder.DropTable(
                name: "VatNumberDefinition");

            migrationBuilder.DropTable(
                name: "RegionHoliday");

            migrationBuilder.DropTable(
                name: "StateHoliday");

            migrationBuilder.DropTable(
                name: "CountryHoliday");
        }
    }
}
