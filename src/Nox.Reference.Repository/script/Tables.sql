CREATE TABLE "Country" (
    "Id" SMALLINT NOT NULL,
    "Name" NVARCHAR(255) NOT NULL,
    "Code" VARCHAR(255) NOT NULL,
    "CommonName" NVARCHAR(255) NOT NULL,
    "OfficialName" NVARCHAR(255) NOT NULL,
    "AlphaCode2" CHAR(2) NOT NULL,
    "NumericCode" SMALLINT NOT NULL,
    "AlphaCode3" CHAR(3) NOT NULL,
    "OlympicCommitteeCode" CHAR(3) NOT NULL,
    "FifaCode" CHAR(3) NOT NULL,
    "FipsCode" CHAR(2) NOT NULL,
    "IsIndependent" BIT NOT NULL,
    "CodeAssignedStatus" VARCHAR(255) NOT NULL,
    "IsUnitedNationsMember" BIT NOT NULL,
    "Region" NVARCHAR(255) NOT NULL,
    "SubRegion" NVARCHAR(255) NULL,
    IsLandLockec BIT NOT NULL,
    LandAreaInSquareKilometers INT,
    EmojiFlag NVARCHAR(255),
    --absent fields
    CONSTRAINT PK_Country PRIMARY KEY (Id)
);

CREATE TABLE "Language" (
    "Id" SMALLINT NOT NULL,
    --absent fields
    CONSTRAINT "PK_Language" PRIMARY KEY (Id)
);

-- CountryLanguage definition

CREATE TABLE "CountryLanguages" (
    "CountryId" SMALLINT NOT NULL,
    "LanguageId" SMALLINT NOT NULL,
    CONSTRAINT "PK_CountryLanguages" PRIMARY KEY ("CountryId", "LanguageId"),
    CONSTRAINT "FK_CountryLanguages_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
    CONSTRAINT "FK_CountryLanguages_Language_LanguageId" FOREIGN KEY ("LanguageId") REFERENCES "Language" ("Id")
);




-- CountryNativeNames definition 

CREATE TABLE "CountryNativeNames" (
    "CountryId" SMALLINT NOT NULL,
    "LanguageId" SMALLINT NOT NULL,
    "OfficialName" NVARCHAR(255) NOT NULL,
    "CommonName" NVARCHAR(255) NOT NULL,
    CONSTRAINT "PK_CountryNativeNames" PRIMARY KEY ("CountryId", "LanguageId"),
    CONSTRAINT "FK_CountryNativeNames_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
    CONSTRAINT "FK_CountryNativeNames_Language_LanguageId" FOREIGN KEY ("LanguageId") REFERENCES "Language" ("Id")
);

CREATE TABLE "DomainNameExtension" (
	"Id" INT NOT NULL,
	"Extension" VARCHAR(25) NOT NULL,
	CONSTRAINT "PK_DomainNameExtension" PRIMARY KEY ("Id")
)

CREATE TABLE CountryDomainNameExtensions (
	CountryId SMALLINT NOT NULL,
	DomainNameExtensionId INT NOT NULL,
	CONSTRAINT "PK_CountryDomainNameExtensions" PRIMARY KEY ("CountryId", "DomainNameExtensionId"),
	CONSTRAINT "FK_CountryDomainNameExtensions_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
    CONSTRAINT "FK_CountryDomainNameExtensions_DomainNameExtension_DomainNameExtensionId" FOREIGN KEY ("DomainNameExtensionId") REFERENCES "DomainNameExtension" ("Id")
)


CREATE TABLE Currency (
	Id SMALLINT NOT NULL,
	CONSTRAINT "PK_Currency" PRIMARY KEY ("Id")
)


CREATE TABLE CountryCurrencies (
	CountryId SMALLINT NOT NULL,
	CurrencyId SMALLINT NOT NULL,
	CONSTRAINT "PK_CountryCurrencies" PRIMARY KEY ("CountryId", "CurrencyId"),
	CONSTRAINT "FK_CountryCurrencies_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
    CONSTRAINT "FK_CountryCurrencies_Currency_CurrencyId" FOREIGN KEY ("CurrencyId") REFERENCES "Currency" ("Id")
)

CREATE TABLE DialingInfo (
	CountryId SMALLINT NOT NULL,
	Prefix CHAR(7) NOT NULL,
	Suffix SMALLINT NOT NULL,
	CONSTRAINT "PK_DialingInfo" PRIMARY KEY ("CountryId", "Prefix", Suffix),
	CONSTRAINT "FK_DialingInfo_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
)


Create TABLE CountryCapitals (
	CountryId SMALLINT NOT NULL,
	GeoPlaceId INT NOT NULL,
	CONSTRAINT "PK_CountryCapitals" PRIMARY KEY ("CountryId", "GeoPlaceId"),
	CONSTRAINT "FK_CountryCapitals_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
	CONSTRAINT "FK_CountryCapitals_GeoPlace_GeoPlaceId" FOREIGN KEY ("GeoPlaceId") REFERENCES "GeoPlace" ("Id")
)

CREATE TABLE GeoPlaceType (
	Id SMALLINT NOT NULL,
	PlaceType VARCHAR(255) NOT NULL
	CONSTRAINT "PK_GeoPlaceType" PRIMARY KEY ("Id")
)

CREATE TABLE GeoPlace (
	Id INT NOT NULL,
	Name NVARCHAR(500) NOT NULL,
	ParentId INT NULL,
	PlaceTypeId SMALLINT NOT NULL,
	Latitude Decimal(8,6) NULL,
	Longitude Decimal(9,6) NULL,
    CONSTRAINT "PK_GeoPlace" PRIMARY KEY (Id),
    CONSTRAINT "FK_GeoPlace_GeoPlaceType_PlaceTypeId" FOREIGN KEY ("PlaceTypeId") REFERENCES "GeoPlaceType" ("Id")
)







CREATE TABLE CountryAlternateSpellings (
	Id SMALLINT NOT NULL,
	CountryId SMALLINT NOT NULL,
	Spelling NVARCHAR(255) NOT NULL,
    CONSTRAINT "PK_CountryAlternateSpellings" PRIMARY KEY (Id),
    CONSTRAINT "FK_CountryAlternateSpellings_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id")
)

CREATE TABLE Continent (
	Id TINYINT NOT NULL,
	PlaceType VARCHAR(255) NOT NULL
	CONSTRAINT "PK_Continent" PRIMARY KEY ("Id"),
)

CREATE TABLE CountryContinents (
	CountryId SMALLINT NOT NULL,
	ContinentId TINYINT NOT NULL,
	CONSTRAINT "PK_CountryContinents" PRIMARY KEY ("CountryId", "ContinentId"),
	CONSTRAINT "FK_CountryContinents_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
	CONSTRAINT "FK_CountryContinents_Continent_ContinentId" FOREIGN KEY ("ContinentId") REFERENCES "Continent" ("Id")
)

CREATE TABLE CountryNameTranslations (
	CountryId SMALLINT NOT NULL,
	LanguageId SMALLINT NOT NULL,
	OfficialName NVARCHAR(255),
	CommonName NVARCHAR(255),
	CONSTRAINT "PK_CountryNameTranslations" PRIMARY KEY ("CountryId", "LanguageId"),
	CONSTRAINT "FK_CountryNameTranslations_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
	CONSTRAINT "FK_CountryNameTranslations_Language_LanguageId" FOREIGN KEY ("LanguageId") REFERENCES "Language" ("Id")
)

CREATE TABLE CountryBorderingCountries (
	CountryId SMALLINT NOT NULL,
	BorderingCountryId SMALLINT NOT NULL,
	CONSTRAINT "PK_CountryBorderingCountries" PRIMARY KEY ("CountryId", "BorderingCountryId"),
	CONSTRAINT "FK_CountryBorderingCountries_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
	CONSTRAINT "FK_CountryBorderingCountries_Country_CountryId2" FOREIGN KEY ("BorderingCountryId") REFERENCES "Country" ("Id")
)


CREATE TABLE Demonym (
	Id SMALLINT NOT NULL,
	CONSTRAINT "PK_Demonym" PRIMARY KEY ("Id")
)

CREATE TABLE CountryDemonyms (
	CountryId SMALLINT NOT NULL,
	DemonymId SMALLINT NOT NULL,
	CONSTRAINT "PK_CountryDemonyms" PRIMARY KEY ("CountryId", "DemonymId"),
	CONSTRAINT "FK_CountryDemonyms_Country_CountryId" FOREIGN KEY ("CountryId") REFERENCES "Country" ("Id"),
	CONSTRAINT "FK_CountryDemonyms_Demonym_DemonymId" FOREIGN KEY ("DemonymId") REFERENCES "Demonym" ("Id")

)

CREATE TABLE DemonymTranslations (
	DemonymId SMALLINT NOT NULL,
	LanguageId SMALLINT NOT NULL,
	Feminine NVARCHAR(255) NOT NULL,
	Masculine NVARCHAR(255) NOT NULL
	CONSTRAINT "PK_DemonymTranslation" PRIMARY KEY ("DemonymId", "LanguageId"),
	CONSTRAINT "FK_DemonymTranslations_Demonym_DemonymId" FOREIGN KEY ("DemonymId") REFERENCES "Demonym" ("Id"),
	CONSTRAINT "FK_DemonymTranslations_Language_LanguageId" FOREIGN KEY ("LanguageId") REFERENCES "Language" ("Id")
)


