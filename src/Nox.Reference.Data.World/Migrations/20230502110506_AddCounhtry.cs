using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nox.Reference.Data.World.Migrations
{
    /// <inheritdoc />
    public partial class AddCounhtry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VatNumberValidationRule",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

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
                    Language = table.Column<string>(type: "TEXT", nullable: false),
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
                name: "IX_CountryNativeName_CountryId",
                table: "CountryNativeName",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryTopLevelDomain_TopLevelDomainsId",
                table: "CountryTopLevelDomain",
                column: "TopLevelDomainsId");

            migrationBuilder.CreateIndex(
                name: "IX_GiniCoefficient_CountryId",
                table: "GiniCoefficient",
                column: "CountryId");
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
                name: "GiniCoefficient");

            migrationBuilder.DropTable(
                name: "Continent");

            migrationBuilder.DropTable(
                name: "Demonymn");

            migrationBuilder.DropTable(
                name: "TopLevelDomain");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "GeoCoordinates");

            migrationBuilder.DropTable(
                name: "PostalCode");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "VatNumberValidationRule",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
