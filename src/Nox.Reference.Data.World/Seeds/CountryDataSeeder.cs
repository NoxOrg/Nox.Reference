using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace Nox.Reference.Data.World;

internal class CountryDataSeeder : INoxReferenceDataSeeder
{
    private readonly IConfiguration _configuration;
    private readonly Mapper _mapper;
    private readonly WorldDbContext _dbContext;
    private readonly ILogger<CountryDataSeeder> _logger;

    public CountryDataSeeder(
        IConfiguration configuration,
        Mapper mapper,
        WorldDbContext dbContext,
        ILogger<CountryDataSeeder> logger)
    {
        _configuration = configuration;
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Seed()
    {
        _logger.LogInformation("Getting country data...");

        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;
        var uriRestCountries = _configuration.GetValue<string>(ConfigurationConstants.UriRestCountriesSettingName)!;
        try
        {
            var data = RestHelper.GetInternetContent(uriRestCountries).Content!;

            var sourceFilePath = Path.Combine(sourceOutputPath, "Countries");
            Directory.CreateDirectory(sourceFilePath);

            var targetFilePath = targetOutputPath;
            Directory.CreateDirectory(targetFilePath);

            // Fix empty dictionaries for 'currencies' from empty arrays
            var editedContent = data.Replace(@"""currencies"": [],", @"""currencies"": {},");

            // save content
            File.WriteAllText(Path.Combine(sourceFilePath, "restcountries.json"), editedContent);

            var countries = JsonSerializer.Deserialize<RestcountryCountryInfo[]>(editedContent) ?? Array.Empty<RestcountryCountryInfo>();

            FixData(countries);
            EnrichWithMappingData(sourceFilePath, countries);
            FixTranslation(_configuration, countries);

            // Store output
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            var outputContent = JsonSerializer.Serialize(countries
                .Where(c => !string.IsNullOrEmpty(c.NumericCode))
                .Cast<ICountryInfo>(),
                options);

            File.WriteAllText(Path.Combine(targetFilePath, "Nox.Reference.Countries.json"), outputContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    private void FixTranslation(IConfiguration configuration, RestcountryCountryInfo[] countries)
    {
        var iso3LanguageData = GetLanguageIso639_3_Data(configuration);
        foreach (var country in countries)
        {
            var dictionaryCopy = country.NameTranslations_.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var countryTranslations = dictionaryCopy.Select(x => x.Key).ToList();

            foreach (var translationLanguage in countryTranslations)
            {
                var countryData = dictionaryCopy[translationLanguage];
                dictionaryCopy.Remove(translationLanguage);

                string? newKey;
                if (translationLanguage == "per")
                {
                    newKey = "fa";
                }
                else
                {
                    newKey = iso3LanguageData.FirstOrDefault(x => translationLanguage.Equals(x.Iso_639_2t))?.Iso_639_1;
                }

                if (string.IsNullOrWhiteSpace(newKey))
                {
                    _logger.LogWarning("Warning! Can't find a language iso 639-1 code for language '{translationLanguage}'.", translationLanguage);
                }
                else
                {
                    dictionaryCopy[newKey!] = countryData;
                }
            }

            dictionaryCopy.Add("en", new RestcountryNativeNameInfo
            {
                CommonName = country.Name,
                OfficialName = country.Name,
                Language = "English",
            });

            country.NameTranslations_ = dictionaryCopy;
        }
    }

    private static void EnrichWithMappingData(string sourceFilePath, RestcountryCountryInfo[] countries)
    {
        var isoAlpha2ToFipsMapping = JsonSerializer.Deserialize<Dictionary<string, string>>(
                        File.ReadAllText(Path.Combine(sourceFilePath, "static-iso2fips.json"))
                    );

        foreach (var country in countries)
        {
            // Add fips codes
            if (isoAlpha2ToFipsMapping?.TryGetValue(country.AlphaCode2, out var fips) ?? false)
            {
                country.FipsCode = fips;
            }

            MapLatLongIntoGeoCoordinates(country);
        }
    }

    private static void FixData(RestcountryCountryInfo[] countries)
    {
        // Edit germany
        var germany = countries.First(c => c.Code.Equals("DEU"));

        if (germany is not null && germany.VehicleInfo_ is not null)
        {
            germany.VehicleInfo_.InternationalRegistrationCodes = new string[] { "D" };
        }
    }

    private static void MapLatLongIntoGeoCoordinates(RestcountryCountryInfo country)
    {
        if (country.LatLong?.Count == 2)
        {
            country.GeoCoordinates.Latitude = country.LatLong[0];
            country.GeoCoordinates.Longitude = country.LatLong[1];
        }
        country.LatLong = null!;

        if (country.CapitalInfo_?.LatLong?.Count == 2)
        {
            country.CapitalInfo_.GeoCoordinates.Latitude = country.CapitalInfo_.LatLong[0];
            country.CapitalInfo_.GeoCoordinates.Longitude = country.CapitalInfo_.LatLong[1];
        }

        if (country.CapitalInfo_ != null)
        {
            country.CapitalInfo_.LatLong = null!;
        }
    }

    public static List<LanguageInfoYaml> GetLanguageIso639_3_Data(
        IConfiguration configuration,
        string? sourceFilePath = null)
    {
        var uriRestLanguages = configuration.GetValue<string>(ConfigurationConstants.UriLanguagesISO639)!;

        var data = RestHelper.GetInternetContent(uriRestLanguages).Content!;

        // Save content
        if (!string.IsNullOrWhiteSpace(sourceFilePath))
        {
            File.WriteAllText(Path.Combine(sourceFilePath, "languages.yml"), data);
        }

        // Remove starting part
        data = data.Replace("---\n", string.Empty);

        var serializer = new Deserializer();
        var splitter = "- :name:";
        var splitContent = data.Split(splitter);
        splitContent = splitContent[1..];

        var languages = new List<LanguageInfoYaml>();

        foreach (var splitPart in splitContent)
        {
            var dataPiece = splitPart;
            var encodeQuotes = false;

            // Handle case when quote is first character
            if (dataPiece.Contains("!"))
            {
                encodeQuotes = true;
                if (dataPiece.Contains(":iso_639_3: alu")) { dataPiece = dataPiece.Replace("! '''Are''are'", "TO_DECODE"); }
                else if (dataPiece.Contains(":iso_639_3: kud")) { dataPiece = dataPiece.Replace("! '''Auhelawa'", "TO_DECODE"); }
                else if (dataPiece.Contains(":iso_639_3: nmn")) { dataPiece = dataPiece.Replace("! '!Xóõ'", "TO_DECODE"); }
                else if (dataPiece.Contains(":iso_639_3: oun")) { dataPiece = dataPiece.Replace("! '!O!ung'", "TO_DECODE"); }
            }

            dataPiece = $"{splitter}{dataPiece}";

            // Remove block
            dataPiece = dataPiece.Replace("-", " ");

            // Replace common name with name
            if (dataPiece.Contains("common_name"))
            {
                dataPiece = string
                    .Join(
                        "\n",
                        dataPiece
                            .Split("\n")
                            .Skip(1))
                    .Replace("common_name", "name");
            }

            dataPiece = dataPiece
                .Replace(":individual", "individual")
                .Replace(":living", "living")
                .Replace(":historical", "historical")
                .Replace(":special", "special")
                .Replace(":extinct", "extinct")
                .Replace(":ancient", "ancient")
                .Replace(":constructed", "constructed")
                .Replace(":macro_language", "macro_language")
                .Replace("'yes'", "yes")
                .Replace("'no'", "no");

            languages.Add(serializer.Deserialize<LanguageInfoYaml>(dataPiece));

            if (encodeQuotes)
            {
                var language = languages[languages.Count - 1];
                if (language.Iso_639_3 == "alu") { languages[languages.Count - 1].EnglishName = "'Are'are"; }
                else if (language.Iso_639_3 == "kud") { languages[languages.Count - 1].EnglishName = "'Auhelawa"; }
                else if (language.Iso_639_3 == "nmn") { languages[languages.Count - 1].EnglishName = "!Xóõ"; }
                else if (language.Iso_639_3 == "oun") { languages[languages.Count - 1].EnglishName = "!O!ung"; }
            }
        }

        return languages;
    }
}