using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nox.Reference.Repository.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "INTEGER", nullable: false),
                    PlaceType = table.Column<string>(type: "TEXT", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "TEXT", unicode: false, maxLength: 255, nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    AlphaCode2 = table.Column<string>(type: "TEXT", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    NumericCode = table.Column<short>(type: "INTEGER", nullable: false),
                    AlphaCode3 = table.Column<string>(type: "TEXT", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    OlympicCommitteeCode = table.Column<string>(type: "TEXT", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    FifaCode = table.Column<string>(type: "TEXT", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    FipsCode = table.Column<string>(type: "TEXT", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    IsIndependent = table.Column<bool>(type: "INTEGER", nullable: false),
                    CodeAssignedStatus = table.Column<string>(type: "TEXT", unicode: false, maxLength: 255, nullable: false),
                    IsUnitedNationsMember = table.Column<bool>(type: "INTEGER", nullable: false),
                    Region = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    SubRegion = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    IsLandLockec = table.Column<bool>(type: "INTEGER", nullable: false),
                    LandAreaInSquareKilometers = table.Column<int>(type: "INTEGER", nullable: true),
                    EmojiFlag = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Demonym",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demonym", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomainNameExtension",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Extension = table.Column<string>(type: "TEXT", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainNameExtension", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoPlaceType",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false),
                    PlaceType = table.Column<string>(type: "TEXT", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoPlaceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryAlternateSpellings",
                columns: table => new
                {
                    Id = table.Column<short>(type: "INTEGER", nullable: false),
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    Spelling = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryAlternateSpellings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryAlternateSpellings_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryBorderingCountries",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    BorderingCountryId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryBorderingCountries", x => new { x.CountryId, x.BorderingCountryId });
                    table.ForeignKey(
                        name: "FK_CountryBorderingCountries_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryBorderingCountries_Country_CountryId2",
                        column: x => x.BorderingCountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryContinents",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    ContinentId = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryContinents", x => new { x.CountryId, x.ContinentId });
                    table.ForeignKey(
                        name: "FK_CountryContinents_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryContinents_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DialingInfo",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", unicode: false, fixedLength: true, maxLength: 7, nullable: false),
                    Suffix = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialingInfo", x => new { x.CountryId, x.Prefix, x.Suffix });
                    table.ForeignKey(
                        name: "FK_DialingInfo_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryCurrencies",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    CurrencyId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCurrencies", x => new { x.CountryId, x.CurrencyId });
                    table.ForeignKey(
                        name: "FK_CountryCurrencies_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryCurrencies_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryDemonyms",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    DemonymId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDemonyms", x => new { x.CountryId, x.DemonymId });
                    table.ForeignKey(
                        name: "FK_CountryDemonyms_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryDemonyms_Demonym_DemonymId",
                        column: x => x.DemonymId,
                        principalTable: "Demonym",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryDomainNameExtensions",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    DomainNameExtensionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDomainNameExtensions", x => new { x.CountryId, x.DomainNameExtensionId });
                    table.ForeignKey(
                        name: "FK_CountryDomainNameExtensions_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryDomainNameExtensions_DomainNameExtension_DomainNameExtensionId",
                        column: x => x.DomainNameExtensionId,
                        principalTable: "DomainNameExtension",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GeoPlace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlaceTypeId = table.Column<short>(type: "INTEGER", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(8, 6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9, 6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoPlace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoPlace_GeoPlaceType_PlaceTypeId",
                        column: x => x.PlaceTypeId,
                        principalTable: "GeoPlaceType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryLanguages",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLanguages", x => new { x.CountryId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CountryLanguages_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryLanguages_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryNameTranslations",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<short>(type: "INTEGER", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CommonName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNameTranslations", x => new { x.CountryId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CountryNameTranslations_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryNameTranslations_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryNativeNames",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<short>(type: "INTEGER", nullable: false),
                    OfficialName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CommonName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryNativeNames", x => new { x.CountryId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CountryNativeNames_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryNativeNames_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DemonymTranslations",
                columns: table => new
                {
                    DemonymId = table.Column<short>(type: "INTEGER", nullable: false),
                    LanguageId = table.Column<short>(type: "INTEGER", nullable: false),
                    Feminine = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Masculine = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemonymTranslation", x => new { x.DemonymId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_DemonymTranslations_Demonym_DemonymId",
                        column: x => x.DemonymId,
                        principalTable: "Demonym",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DemonymTranslations_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryCapitals",
                columns: table => new
                {
                    CountryId = table.Column<short>(type: "INTEGER", nullable: false),
                    GeoPlaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCapitals", x => new { x.CountryId, x.GeoPlaceId });
                    table.ForeignKey(
                        name: "FK_CountryCapitals_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryCapitals_GeoPlace_GeoPlaceId",
                        column: x => x.GeoPlaceId,
                        principalTable: "GeoPlace",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CountryAlternateSpellings_CountryId",
                table: "CountryAlternateSpellings",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryBorderingCountries_BorderingCountryId",
                table: "CountryBorderingCountries",
                column: "BorderingCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCapitals_GeoPlaceId",
                table: "CountryCapitals",
                column: "GeoPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryContinents_ContinentId",
                table: "CountryContinents",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrencies_CurrencyId",
                table: "CountryCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryDemonyms_DemonymId",
                table: "CountryDemonyms",
                column: "DemonymId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryDomainNameExtensions_DomainNameExtensionId",
                table: "CountryDomainNameExtensions",
                column: "DomainNameExtensionId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguages_LanguageId",
                table: "CountryLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNameTranslations_LanguageId",
                table: "CountryNameTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryNativeNames_LanguageId",
                table: "CountryNativeNames",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_DemonymTranslations_LanguageId",
                table: "DemonymTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_GeoPlace_PlaceTypeId",
                table: "GeoPlace",
                column: "PlaceTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryAlternateSpellings");

            migrationBuilder.DropTable(
                name: "CountryBorderingCountries");

            migrationBuilder.DropTable(
                name: "CountryCapitals");

            migrationBuilder.DropTable(
                name: "CountryContinents");

            migrationBuilder.DropTable(
                name: "CountryCurrencies");

            migrationBuilder.DropTable(
                name: "CountryDemonyms");

            migrationBuilder.DropTable(
                name: "CountryDomainNameExtensions");

            migrationBuilder.DropTable(
                name: "CountryLanguages");

            migrationBuilder.DropTable(
                name: "CountryNameTranslations");

            migrationBuilder.DropTable(
                name: "CountryNativeNames");

            migrationBuilder.DropTable(
                name: "DemonymTranslations");

            migrationBuilder.DropTable(
                name: "DialingInfo");

            migrationBuilder.DropTable(
                name: "GeoPlace");

            migrationBuilder.DropTable(
                name: "Continent");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "DomainNameExtension");

            migrationBuilder.DropTable(
                name: "Demonym");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "GeoPlaceType");
        }
    }
}
