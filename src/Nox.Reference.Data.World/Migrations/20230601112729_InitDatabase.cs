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
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Svg = table.Column<string>(type: "TEXT", nullable: false),
                    Png = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoatOfArms", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "CountryDialing",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prefix = table.Column<string>(type: "TEXT", nullable: false),
                    Suffixes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDialing", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "CountryFlag",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Svg = table.Column<string>(type: "TEXT", nullable: false),
                    Png = table.Column<string>(type: "TEXT", nullable: false),
                    AlternateText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryFlag", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "CountryMaps",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoogleMaps = table.Column<string>(type: "TEXT", nullable: false),
                    OpenStreetMaps = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMaps", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "CountryNames",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNames", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "CountryVehicle",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DrivingSide = table.Column<string>(type: "TEXT", nullable: false),
                    InternationalRegistrationCodes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryVehicle", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Culture",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_Culture", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyUsage",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyUsage", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "GeoCoordinates",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: true),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoCoordinates", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_Language", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "MajorCurrencyUnit",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorCurrencyUnit", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "MinorCurrencyUnit",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    MajorValue = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinorCurrencyUnit", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "PhoneCarrier",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneCarrier", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "PostalCode",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Format = table.Column<string>(type: "TEXT", nullable: true),
                    Regex = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCode", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "TimeZone",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_TimeZone", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "TopLevelDomain",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopLevelDomain", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "VatNumberDefinition",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    LocalName = table.Column<string>(type: "TEXT", nullable: false),
                    VerificationApi = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatNumberDefinition", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "DateFormat",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_DateFormat", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_DateFormat_Culture_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Culture",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NumberFormat",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_NumberFormat", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_NumberFormat_Culture_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Culture",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyFrequentUsage",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyUsageEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyFrequentUsage", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_CurrencyFrequentUsage_CurrencyUsage_CurrencyUsageEntityId",
                        column: x => x.CurrencyUsageEntityId,
                        principalTable: "CurrencyUsage",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRareUsage",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyUsageEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRareUsage", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_CurrencyRareUsage_CurrencyUsage_CurrencyUsageEntityId",
                        column: x => x.CurrencyUsageEntityId,
                        principalTable: "CurrencyUsage",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryNativeName",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LanguageEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryNamesEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNativeName", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_CountryNativeName_CountryNames_CountryNamesEntityId",
                        column: x => x.CountryNamesEntityId,
                        principalTable: "CountryNames",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_CountryNativeName_Language_LanguageEntityId",
                        column: x => x.LanguageEntityId,
                        principalTable: "Language",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Demonymn",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LanguageEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Feminine = table.Column<string>(type: "TEXT", nullable: false),
                    Masculine = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demonymn", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_Demonymn_Language_LanguageEntityId",
                        column: x => x.LanguageEntityId,
                        principalTable: "Language",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTranslation",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Translation = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    LanguageEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTranslation", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_LanguageTranslation_Language_LanguageEntityId",
                        column: x => x.LanguageEntityId,
                        principalTable: "Language",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    BanknotesEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoinsEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    MajorUnitEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    MinorUnitEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_Currency_CurrencyUsage_BanknotesEntityId",
                        column: x => x.BanknotesEntityId,
                        principalTable: "CurrencyUsage",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Currency_CurrencyUsage_CoinsEntityId",
                        column: x => x.CoinsEntityId,
                        principalTable: "CurrencyUsage",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Currency_MajorCurrencyUnit_MajorUnitEntityId",
                        column: x => x.MajorUnitEntityId,
                        principalTable: "MajorCurrencyUnit",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Currency_MinorCurrencyUnit_MinorUnitEntityId",
                        column: x => x.MinorUnitEntityId,
                        principalTable: "MinorCurrencyUnit",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrierPhoneNumber",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhoneNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneCarrierEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierPhoneNumber", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_CarrierPhoneNumber_PhoneCarrier_PhoneCarrierEntityId",
                        column: x => x.PhoneCarrierEntityId,
                        principalTable: "PhoneCarrier",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    NamesEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    DialingEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    CoatOfArmsEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    GeoCoordinatesEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    FlagEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    MapsEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    VehicleEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    PostalCodeEntityId = table.Column<int>(type: "INTEGER", nullable: true),
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
                    table.PrimaryKey("PK_Country", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_Country_CoatOfArms_CoatOfArmsEntityId",
                        column: x => x.CoatOfArmsEntityId,
                        principalTable: "CoatOfArms",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_Country_CountryDialing_DialingEntityId",
                        column: x => x.DialingEntityId,
                        principalTable: "CountryDialing",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_Country_CountryFlag_FlagEntityId",
                        column: x => x.FlagEntityId,
                        principalTable: "CountryFlag",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_Country_CountryMaps_MapsEntityId",
                        column: x => x.MapsEntityId,
                        principalTable: "CountryMaps",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_Country_CountryNames_NamesEntityId",
                        column: x => x.NamesEntityId,
                        principalTable: "CountryNames",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Country_CountryVehicle_VehicleEntityId",
                        column: x => x.VehicleEntityId,
                        principalTable: "CountryVehicle",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_Country_GeoCoordinates_GeoCoordinatesEntityId",
                        column: x => x.GeoCoordinatesEntityId,
                        principalTable: "GeoCoordinates",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_Country_PostalCode_PostalCodeEntityId",
                        column: x => x.PostalCodeEntityId,
                        principalTable: "PostalCode",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "VatNumberValidationRule",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    VatNumberDefinitionEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatNumberValidationRule", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_VatNumberValidationRule_VatNumberDefinition_VatNumberDefinitionEntityId",
                        column: x => x.VatNumberDefinitionEntityId,
                        principalTable: "VatNumberDefinition",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "AlternateSpelling",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateSpelling", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_AlternateSpelling_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "ContinentCountry",
                columns: table => new
                {
                    ContinentsEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentCountry", x => new { x.ContinentsEntityId, x.CountryEntityId });
                    table.ForeignKey(
                        name: "FK_ContinentCountry_Continent_ContinentsEntityId",
                        column: x => x.ContinentsEntityId,
                        principalTable: "Continent",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinentCountry_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryCapital",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GeoCoordinatesEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCapital", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_CountryCapital_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_CountryCapital_GeoCoordinates_GeoCoordinatesEntityId",
                        column: x => x.GeoCoordinatesEntityId,
                        principalTable: "GeoCoordinates",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "CountryCountry",
                columns: table => new
                {
                    BorderingCountriesEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCountry", x => new { x.BorderingCountriesEntityId, x.CountryEntityId });
                    table.ForeignKey(
                        name: "FK_CountryCountry_Country_BorderingCountriesEntityId",
                        column: x => x.BorderingCountriesEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryCountry_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryCurrency",
                columns: table => new
                {
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrenciesEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCurrency", x => new { x.CountryEntityId, x.CurrenciesEntityId });
                    table.ForeignKey(
                        name: "FK_CountryCurrency_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryCurrency_Currency_CurrenciesEntityId",
                        column: x => x.CurrenciesEntityId,
                        principalTable: "Currency",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryDemonymn",
                columns: table => new
                {
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    DemonymsEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDemonymn", x => new { x.CountryEntityId, x.DemonymsEntityId });
                    table.ForeignKey(
                        name: "FK_CountryDemonymn_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryDemonymn_Demonymn_DemonymsEntityId",
                        column: x => x.DemonymsEntityId,
                        principalTable: "Demonymn",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryHoliday",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryName = table.Column<string>(type: "TEXT", nullable: true),
                    DayOff = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryHoliday", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_CountryHoliday_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryLanguage",
                columns: table => new
                {
                    CountriesEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguagesEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLanguage", x => new { x.CountriesEntityId, x.LanguagesEntityId });
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Country_CountriesEntityId",
                        column: x => x.CountriesEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Language_LanguagesEntityId",
                        column: x => x.LanguagesEntityId,
                        principalTable: "Language",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryNameTranslation",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNameTranslation", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_CountryNameTranslation_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryNameTranslation_Language_LanguageEntityId",
                        column: x => x.LanguageEntityId,
                        principalTable: "Language",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryTimeZone",
                columns: table => new
                {
                    CountriesEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeZonesEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTimeZone", x => new { x.CountriesEntityId, x.TimeZonesEntityId });
                    table.ForeignKey(
                        name: "FK_CountryTimeZone_Country_CountriesEntityId",
                        column: x => x.CountriesEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryTimeZone_TimeZone_TimeZonesEntityId",
                        column: x => x.TimeZonesEntityId,
                        principalTable: "TimeZone",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryTopLevelDomain",
                columns: table => new
                {
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    TopLevelDomainsEntityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTopLevelDomain", x => new { x.CountryEntityId, x.TopLevelDomainsEntityId });
                    table.ForeignKey(
                        name: "FK_CountryTopLevelDomain_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryTopLevelDomain_TopLevelDomain_TopLevelDomainsEntityId",
                        column: x => x.TopLevelDomainsEntityId,
                        principalTable: "TopLevelDomain",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiniCoefficient",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    CountryEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiniCoefficient", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_GiniCoefficient_Country_CountryEntityId",
                        column: x => x.CountryEntityId,
                        principalTable: "Country",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "StateHoliday",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    StateName = table.Column<string>(type: "TEXT", nullable: false),
                    CountryHolidayEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateHoliday", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_StateHoliday_CountryHoliday_CountryHolidayEntityId",
                        column: x => x.CountryHolidayEntityId,
                        principalTable: "CountryHoliday",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "RegionHoliday",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Region = table.Column<string>(type: "TEXT", nullable: false),
                    RegionName = table.Column<string>(type: "TEXT", nullable: false),
                    StateHolidayEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionHoliday", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_RegionHoliday_StateHoliday_StateHolidayEntityId",
                        column: x => x.StateHolidayEntityId,
                        principalTable: "StateHoliday",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "HolidayData",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    CountryHolidayEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    RegionHolidayEntityId = table.Column<int>(type: "INTEGER", nullable: true),
                    StateHolidayEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayData", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_HolidayData_CountryHoliday_CountryHolidayEntityId",
                        column: x => x.CountryHolidayEntityId,
                        principalTable: "CountryHoliday",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_HolidayData_RegionHoliday_RegionHolidayEntityId",
                        column: x => x.RegionHolidayEntityId,
                        principalTable: "RegionHoliday",
                        principalColumn: "EntityId");
                    table.ForeignKey(
                        name: "FK_HolidayData_StateHoliday_StateHolidayEntityId",
                        column: x => x.StateHolidayEntityId,
                        principalTable: "StateHoliday",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateTable(
                name: "LocalHolidayName",
                columns: table => new
                {
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Language = table.Column<string>(type: "TEXT", nullable: true),
                    HolidayDataEntityId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalHolidayName", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_LocalHolidayName_HolidayData_HolidayDataEntityId",
                        column: x => x.HolidayDataEntityId,
                        principalTable: "HolidayData",
                        principalColumn: "EntityId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternateSpelling_CountryEntityId",
                table: "AlternateSpelling",
                column: "CountryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierPhoneNumber_PhoneCarrierEntityId",
                table: "CarrierPhoneNumber",
                column: "PhoneCarrierEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentCountry_CountryEntityId",
                table: "ContinentCountry",
                column: "CountryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CoatOfArmsEntityId",
                table: "Country",
                column: "CoatOfArmsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_DialingEntityId",
                table: "Country",
                column: "DialingEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_FlagEntityId",
                table: "Country",
                column: "FlagEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_GeoCoordinatesEntityId",
                table: "Country",
                column: "GeoCoordinatesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_MapsEntityId",
                table: "Country",
                column: "MapsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_NamesEntityId",
                table: "Country",
                column: "NamesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_PostalCodeEntityId",
                table: "Country",
                column: "PostalCodeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_VehicleEntityId",
                table: "Country",
                column: "VehicleEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCapital_CountryEntityId",
                table: "CountryCapital",
                column: "CountryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCapital_GeoCoordinatesEntityId",
                table: "CountryCapital",
                column: "GeoCoordinatesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCountry_CountryEntityId",
                table: "CountryCountry",
                column: "CountryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrency_CurrenciesEntityId",
                table: "CountryCurrency",
                column: "CurrenciesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryDemonymn_DemonymsEntityId",
                table: "CountryDemonymn",
                column: "DemonymsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryHoliday_CountryEntityId",
                table: "CountryHoliday",
                column: "CountryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguage_LanguagesEntityId",
                table: "CountryLanguage",
                column: "LanguagesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNameTranslation_CountryEntityId",
                table: "CountryNameTranslation",
                column: "CountryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNameTranslation_LanguageEntityId",
                table: "CountryNameTranslation",
                column: "LanguageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNativeName_CountryNamesEntityId",
                table: "CountryNativeName",
                column: "CountryNamesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNativeName_LanguageEntityId",
                table: "CountryNativeName",
                column: "LanguageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTimeZone_TimeZonesEntityId",
                table: "CountryTimeZone",
                column: "TimeZonesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTopLevelDomain_TopLevelDomainsEntityId",
                table: "CountryTopLevelDomain",
                column: "TopLevelDomainsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_BanknotesEntityId",
                table: "Currency",
                column: "BanknotesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_CoinsEntityId",
                table: "Currency",
                column: "CoinsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_MajorUnitEntityId",
                table: "Currency",
                column: "MajorUnitEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_MinorUnitEntityId",
                table: "Currency",
                column: "MinorUnitEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyFrequentUsage_CurrencyUsageEntityId",
                table: "CurrencyFrequentUsage",
                column: "CurrencyUsageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRareUsage_CurrencyUsageEntityId",
                table: "CurrencyRareUsage",
                column: "CurrencyUsageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DateFormat_CultureId",
                table: "DateFormat",
                column: "CultureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demonymn_LanguageEntityId",
                table: "Demonymn",
                column: "LanguageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_GiniCoefficient_CountryEntityId",
                table: "GiniCoefficient",
                column: "CountryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayData_CountryHolidayEntityId",
                table: "HolidayData",
                column: "CountryHolidayEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayData_RegionHolidayEntityId",
                table: "HolidayData",
                column: "RegionHolidayEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayData_StateHolidayEntityId",
                table: "HolidayData",
                column: "StateHolidayEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTranslation_LanguageEntityId",
                table: "LanguageTranslation",
                column: "LanguageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalHolidayName_HolidayDataEntityId",
                table: "LocalHolidayName",
                column: "HolidayDataEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberFormat_CultureId",
                table: "NumberFormat",
                column: "CultureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegionHoliday_StateHolidayEntityId",
                table: "RegionHoliday",
                column: "StateHolidayEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_StateHoliday_CountryHolidayEntityId",
                table: "StateHoliday",
                column: "CountryHolidayEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_VatNumberValidationRule_VatNumberDefinitionEntityId",
                table: "VatNumberValidationRule",
                column: "VatNumberDefinitionEntityId");
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
