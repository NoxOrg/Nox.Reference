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
                name: "Continent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.Id);
                });

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
                name: "Demonymn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    Feminine = table.Column<string>(type: "TEXT", nullable: false),
                    Masculine = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demonymn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoCoordinates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: true),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoCoordinates", x => x.Id);
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
                name: "PostalCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Format = table.Column<string>(type: "TEXT", nullable: false),
                    Regex = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopLevelDomain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopLevelDomain", x => x.Id);
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
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false),
                    Dialing_Prefix = table.Column<string>(type: "TEXT", nullable: false),
                    Dialing_Suffixes = table.Column<string>(type: "TEXT", nullable: false),
                    CoatOfArms_Svg = table.Column<string>(type: "TEXT", nullable: false),
                    CoatOfArms_Png = table.Column<string>(type: "TEXT", nullable: false),
                    GeoCoordinatesId = table.Column<int>(type: "INTEGER", nullable: false),
                    Flag_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Flag_Svg = table.Column<string>(type: "TEXT", nullable: false),
                    Flag_Png = table.Column<string>(type: "TEXT", nullable: false),
                    Flag_AlternateText = table.Column<string>(type: "TEXT", nullable: false),
                    Maps_GoogleMaps = table.Column<string>(type: "TEXT", nullable: false),
                    Maps_OpenStreetMaps = table.Column<string>(type: "TEXT", nullable: false),
                    Vehicle_DrivingSide = table.Column<string>(type: "TEXT", nullable: false),
                    Vehicle_InternationalRegistrationCodes = table.Column<string>(type: "TEXT", nullable: false),
                    PostalCodeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmojiFlag = table.Column<string>(type: "TEXT", nullable: false),
                    LandAreaInSquareKilometers = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsIndependent = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsUnitedNationsMember = table.Column<bool>(type: "INTEGER", nullable: false),
                    Region = table.Column<string>(type: "TEXT", nullable: false),
                    SubRegion = table.Column<string>(type: "TEXT", nullable: false),
                    IsLandlocked = table.Column<bool>(type: "INTEGER", nullable: false),
                    Population = table.Column<decimal>(type: "TEXT", nullable: false),
                    StartOfWeek = table.Column<string>(type: "TEXT", nullable: false),
                    AlphaCode2 = table.Column<string>(type: "TEXT", nullable: false),
                    NumericCode = table.Column<string>(type: "TEXT", nullable: false),
                    AlphaCode3 = table.Column<string>(type: "TEXT", nullable: false),
                    OlympicCommitteeCode = table.Column<string>(type: "TEXT", nullable: false),
                    FifaCode = table.Column<string>(type: "TEXT", nullable: false),
                    FipsCode = table.Column<string>(type: "TEXT", nullable: false),
                    CodeAssignedStatus = table.Column<string>(type: "TEXT", nullable: false),
                    StartDayOfWeek = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_GeoCoordinates_GeoCoordinatesId",
                        column: x => x.GeoCoordinatesId,
                        principalTable: "GeoCoordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Country_PostalCode_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCode",
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
                name: "AlternateSpelling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateSpelling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateSpelling_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContinentCountry",
                columns: table => new
                {
                    ContinentsId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentCountry", x => new { x.ContinentsId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_ContinentCountry_Continent_ContinentsId",
                        column: x => x.ContinentsId,
                        principalTable: "Continent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinentCountry_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryCapital",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GeoCoordinatesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCapital", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryCapital_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryCapital_GeoCoordinates_GeoCoordinatesId",
                        column: x => x.GeoCoordinatesId,
                        principalTable: "GeoCoordinates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryCountry",
                columns: table => new
                {
                    BorderingCountriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCountry", x => new { x.BorderingCountriesId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_CountryCountry_Country_BorderingCountriesId",
                        column: x => x.BorderingCountriesId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryCountry_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryCurrency",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrenciesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCurrency", x => new { x.CountryId, x.CurrenciesId });
                    table.ForeignKey(
                        name: "FK_CountryCurrency_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryCurrency_Currency_CurrenciesId",
                        column: x => x.CurrenciesId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryDemonymn",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    DemonymsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDemonymn", x => new { x.CountryId, x.DemonymsId });
                    table.ForeignKey(
                        name: "FK_CountryDemonymn_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryDemonymn_Demonymn_DemonymsId",
                        column: x => x.DemonymsId,
                        principalTable: "Demonymn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryLanguage",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguagesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLanguage", x => new { x.CountryId, x.LanguagesId });
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Language_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryNameTranslation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNameTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryNameTranslation_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryNameTranslation_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryNativeName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNativeName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryNativeName_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryTopLevelDomain",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    TopLevelDomainsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTopLevelDomain", x => new { x.CountryId, x.TopLevelDomainsId });
                    table.ForeignKey(
                        name: "FK_CountryTopLevelDomain_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryTopLevelDomain_TopLevelDomain_TopLevelDomainsId",
                        column: x => x.TopLevelDomainsId,
                        principalTable: "TopLevelDomain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiniCoefficient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiniCoefficient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiniCoefficient_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
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
                name: "IX_AlternateSpelling_CountryId",
                table: "AlternateSpelling",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentCountry_CountryId",
                table: "ContinentCountry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_GeoCoordinatesId",
                table: "Country",
                column: "GeoCoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_PostalCodeId",
                table: "Country",
                column: "PostalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCapital_CountryId",
                table: "CountryCapital",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCapital_GeoCoordinatesId",
                table: "CountryCapital",
                column: "GeoCoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCountry_CountryId",
                table: "CountryCountry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrency_CurrenciesId",
                table: "CountryCurrency",
                column: "CurrenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryDemonymn_DemonymsId",
                table: "CountryDemonymn",
                column: "DemonymsId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguage_LanguagesId",
                table: "CountryLanguage",
                column: "LanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNameTranslation_CountryId",
                table: "CountryNameTranslation",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNameTranslation_LanguageId",
                table: "CountryNameTranslation",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNativeName_CountryId",
                table: "CountryNativeName",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTopLevelDomain_TopLevelDomainsId",
                table: "CountryTopLevelDomain",
                column: "TopLevelDomainsId");

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
                name: "IX_GiniCoefficient_CountryId",
                table: "GiniCoefficient",
                column: "CountryId");

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
                name: "AlternateSpelling");

            migrationBuilder.DropTable(
                name: "ContinentCountry");

            migrationBuilder.DropTable(
                name: "CountryCapital");

            migrationBuilder.DropTable(
                name: "CountryCountry");

            migrationBuilder.DropTable(
                name: "CountryCurrency");

            migrationBuilder.DropTable(
                name: "CountryDemonymn");

            migrationBuilder.DropTable(
                name: "CountryLanguage");

            migrationBuilder.DropTable(
                name: "CountryNameTranslation");

            migrationBuilder.DropTable(
                name: "CountryNativeName");

            migrationBuilder.DropTable(
                name: "CountryTopLevelDomain");

            migrationBuilder.DropTable(
                name: "CurrencyFrequentUsage");

            migrationBuilder.DropTable(
                name: "CurrencyRareUsage");

            migrationBuilder.DropTable(
                name: "GiniCoefficient");

            migrationBuilder.DropTable(
                name: "LanguageTranslation");

            migrationBuilder.DropTable(
                name: "LocalHolidayName");

            migrationBuilder.DropTable(
                name: "VatNumberValidationRule");

            migrationBuilder.DropTable(
                name: "Continent");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Demonymn");

            migrationBuilder.DropTable(
                name: "TopLevelDomain");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "HolidayData");

            migrationBuilder.DropTable(
                name: "VatNumberDefinition");

            migrationBuilder.DropTable(
                name: "CurrencyUsage");

            migrationBuilder.DropTable(
                name: "MajorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "MinorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "GeoCoordinates");

            migrationBuilder.DropTable(
                name: "PostalCode");

            migrationBuilder.DropTable(
                name: "RegionHoliday");

            migrationBuilder.DropTable(
                name: "StateHoliday");

            migrationBuilder.DropTable(
                name: "CountryHoliday");
        }
    }
}
