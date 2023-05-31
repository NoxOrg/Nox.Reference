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
                name: "CoatOfArms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Svg = table.Column<string>(type: "TEXT", nullable: false),
                    Png = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoatOfArms", x => x.Id);
                });

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
                name: "CountryDialing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prefix = table.Column<string>(type: "TEXT", nullable: false),
                    Suffixes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDialing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryFlag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Svg = table.Column<string>(type: "TEXT", nullable: false),
                    Png = table.Column<string>(type: "TEXT", nullable: false),
                    AlternateText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryFlag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoogleMaps = table.Column<string>(type: "TEXT", nullable: false),
                    OpenStreetMaps = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMaps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryVehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DrivingSide = table.Column<string>(type: "TEXT", nullable: false),
                    InternationalRegistrationCodes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryVehicle", x => x.Id);
                });

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
                name: "PhoneCarrier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneCarrier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostalCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Format = table.Column<string>(type: "TEXT", nullable: true),
                    Regex = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeZone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    EmbeddedComments = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    SDT_UTC_Offset = table.Column<string>(type: "TEXT", nullable: false),
                    DST_UTC_Offset = table.Column<string>(type: "TEXT", nullable: false),
                    SDT_TimeZoneAbbreviation = table.Column<string>(type: "TEXT", nullable: false),
                    DST_TimeZoneAbbreviation = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeZone", x => x.Id);
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
                    CultureId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "CountryNativeName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryNamesId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNativeName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryNativeName_CountryNames_CountryNamesId",
                        column: x => x.CountryNamesId,
                        principalTable: "CountryNames",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryNativeName_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Demonymn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Feminine = table.Column<string>(type: "TEXT", nullable: false),
                    Masculine = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demonymn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Demonymn_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
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
                name: "CarrierPhoneNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhoneNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneCarrierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierPhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierPhoneNumber_PhoneCarrier_PhoneCarrierId",
                        column: x => x.PhoneCarrierId,
                        principalTable: "PhoneCarrier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    NamesId = table.Column<int>(type: "INTEGER", nullable: false),
                    DialingId = table.Column<int>(type: "INTEGER", nullable: true),
                    CoatOfArmsId = table.Column<int>(type: "INTEGER", nullable: true),
                    GeoCoordinatesId = table.Column<int>(type: "INTEGER", nullable: true),
                    FlagId = table.Column<int>(type: "INTEGER", nullable: true),
                    MapsId = table.Column<int>(type: "INTEGER", nullable: true),
                    VehicleId = table.Column<int>(type: "INTEGER", nullable: true),
                    PostalCodeId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        name: "FK_Country_CoatOfArms_CoatOfArmsId",
                        column: x => x.CoatOfArmsId,
                        principalTable: "CoatOfArms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_CountryDialing_DialingId",
                        column: x => x.DialingId,
                        principalTable: "CountryDialing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_CountryFlag_FlagId",
                        column: x => x.FlagId,
                        principalTable: "CountryFlag",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_CountryMaps_MapsId",
                        column: x => x.MapsId,
                        principalTable: "CountryMaps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_CountryNames_NamesId",
                        column: x => x.NamesId,
                        principalTable: "CountryNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Country_CountryVehicle_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "CountryVehicle",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_GeoCoordinates_GeoCoordinatesId",
                        column: x => x.GeoCoordinatesId,
                        principalTable: "GeoCoordinates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_PostalCode_PostalCodeId",
                        column: x => x.PostalCodeId,
                        principalTable: "PostalCode",
                        principalColumn: "Id");
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
                    GeoCoordinatesId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        principalColumn: "Id");
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
                name: "CountryHoliday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryName = table.Column<string>(type: "TEXT", nullable: true),
                    DayOff = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryHoliday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryHoliday_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryLanguage",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguagesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLanguage", x => new { x.CountriesId, x.LanguagesId });
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Country_CountriesId",
                        column: x => x.CountriesId,
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
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNameTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryNameTranslation_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryNameTranslation_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryTimeZone",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeZonesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTimeZone", x => new { x.CountriesId, x.TimeZonesId });
                    table.ForeignKey(
                        name: "FK_CountryTimeZone_Country_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryTimeZone_TimeZone_TimeZonesId",
                        column: x => x.TimeZonesId,
                        principalTable: "TimeZone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_AlternateSpelling_CountryId",
                table: "AlternateSpelling",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierPhoneNumber_PhoneCarrierId",
                table: "CarrierPhoneNumber",
                column: "PhoneCarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentCountry_CountryId",
                table: "ContinentCountry",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CoatOfArmsId",
                table: "Country",
                column: "CoatOfArmsId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_DialingId",
                table: "Country",
                column: "DialingId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_FlagId",
                table: "Country",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_GeoCoordinatesId",
                table: "Country",
                column: "GeoCoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_MapsId",
                table: "Country",
                column: "MapsId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_NamesId",
                table: "Country",
                column: "NamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_PostalCodeId",
                table: "Country",
                column: "PostalCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_VehicleId",
                table: "Country",
                column: "VehicleId");

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
                name: "IX_CountryHoliday_CountryId",
                table: "CountryHoliday",
                column: "CountryId");

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
                name: "IX_CountryNativeName_CountryNamesId",
                table: "CountryNativeName",
                column: "CountryNamesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNativeName_LanguageId",
                table: "CountryNativeName",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTimeZone_TimeZonesId",
                table: "CountryTimeZone",
                column: "TimeZonesId");

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
                name: "IX_DateFormat_CultureId",
                table: "DateFormat",
                column: "CultureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demonymn_LanguageId",
                table: "Demonymn",
                column: "LanguageId");

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
                name: "IX_NumberFormat_CultureId",
                table: "NumberFormat",
                column: "CultureId",
                unique: true);

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
                name: "CarrierPhoneNumber");

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
                name: "CountryTimeZone");

            migrationBuilder.DropTable(
                name: "CountryTopLevelDomain");

            migrationBuilder.DropTable(
                name: "CurrencyFrequentUsage");

            migrationBuilder.DropTable(
                name: "CurrencyRareUsage");

            migrationBuilder.DropTable(
                name: "DateFormat");

            migrationBuilder.DropTable(
                name: "GiniCoefficient");

            migrationBuilder.DropTable(
                name: "LanguageTranslation");

            migrationBuilder.DropTable(
                name: "LocalHolidayName");

            migrationBuilder.DropTable(
                name: "NumberFormat");

            migrationBuilder.DropTable(
                name: "VatNumberValidationRule");

            migrationBuilder.DropTable(
                name: "PhoneCarrier");

            migrationBuilder.DropTable(
                name: "Continent");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Demonymn");

            migrationBuilder.DropTable(
                name: "TimeZone");

            migrationBuilder.DropTable(
                name: "TopLevelDomain");

            migrationBuilder.DropTable(
                name: "HolidayData");

            migrationBuilder.DropTable(
                name: "Culture");

            migrationBuilder.DropTable(
                name: "VatNumberDefinition");

            migrationBuilder.DropTable(
                name: "CurrencyUsage");

            migrationBuilder.DropTable(
                name: "MajorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "MinorCurrencyUnit");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "RegionHoliday");

            migrationBuilder.DropTable(
                name: "StateHoliday");

            migrationBuilder.DropTable(
                name: "CountryHoliday");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "CoatOfArms");

            migrationBuilder.DropTable(
                name: "CountryDialing");

            migrationBuilder.DropTable(
                name: "CountryFlag");

            migrationBuilder.DropTable(
                name: "CountryMaps");

            migrationBuilder.DropTable(
                name: "CountryNames");

            migrationBuilder.DropTable(
                name: "CountryVehicle");

            migrationBuilder.DropTable(
                name: "GeoCoordinates");

            migrationBuilder.DropTable(
                name: "PostalCode");
        }
    }
}
