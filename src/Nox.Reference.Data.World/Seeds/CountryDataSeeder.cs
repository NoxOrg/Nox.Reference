using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.Common.Seeds;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace Nox.Reference.Data.World;

internal class CountryDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, CountryInfo, Country>
{
    private readonly IConfiguration _configuration;

    public CountryDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        WorldDbContext dbContext,
        ILogger<CountryDataSeeder> logger,
        NoxReferenceFileStorageService fileStorageService)
        : base(dbContext, mapper, logger, fileStorageService)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.Countries.json";
    public override string DataFolderPath => "Countries";

    protected override IReadOnlyList<CountryInfo> GetFlatEntitiesFromDataSources()
    {
        var uriRestCountries = _configuration.GetValue<string>(ConfigurationConstants.UriRestCountriesSettingName)!;
        var data = RestHelper.GetInternetContent(uriRestCountries).Content!;

        // Fix empty dictionaries for 'currencies' from empty arrays
        var editedContent = data.Replace(@"""currencies"": [],", @"""currencies"": {},");

        // save content
        _fileStorageService.SaveContentToSource(editedContent, DataFolderPath, "restcountries.json");

        var countries = JsonSerializer.Deserialize<CountryInfo[]>(editedContent) ?? Array.Empty<CountryInfo>();

        FixData(countries);
        EnrichWithMappingData(countries);
        FixTranslation(_configuration, countries);

        return countries
            .Where(c => !string.IsNullOrEmpty(c.NumericCode))
            .ToList();
    }

    protected override void DoSpecialTreatAfterAdding(IEnumerable<CountryInfo> sources, IEnumerable<Country> destinations)
    {
        base.DoSpecialTreatAfterAdding(sources, destinations);

        var continents = _mapper.Map<List<Continent>>(sources.SelectMany(x => x.Continents).Distinct().ToArray());
        _dbContext.AddRange(continents);

        var languages = _dbContext.Set<Language>().ToList();
        var currencies = _dbContext.Set<Currency>().ToList();

        foreach (var source in sources)
        {
            var countryEntity = destinations.First(x => x.Code == source.Code);
            countryEntity.BorderingCountries = destinations
                .Where(x => source.BorderingCountries.Contains(x.Code))
                .ToList();

            countryEntity.NameTranslations = _mapper.Map<List<CountryNameTranslation>>(source.NameTranslations);
            countryEntity.Continents = continents.Where(x => source.Continents.Contains(x.Name)).ToList();
            countryEntity.AlternateSpellings = _mapper.Map<List<AlternateSpelling>>(source.AlternateSpellings);
            countryEntity.Demonyms = _mapper.Map<List<Demonymn>>(source.Demonyms);
            countryEntity.TopLevelDomains = _mapper.Map<List<TopLevelDomain>>(source.TopLevelDomains);
            countryEntity.Languages = languages.Where(x => source.Languages.Contains(x.Iso_639_3)).ToList();
            countryEntity.Currencies = currencies.Where(x => source.Currencies.Contains(x.IsoCode)).ToList();
        }

        _dbContext.Set<Country>()
            .UpdateRange(destinations);

        _dbContext.SaveChanges();

        EnumGeneratorService.Generate(destinations, x => x.Name, "World", "WorldCountries");
    }

    private void FixTranslation(IConfiguration configuration, CountryInfo[] countries)
    {
        var iso3LanguageData = GetLanguageIso639_3_Data(configuration);
        foreach (var country in countries)
        {
            var dictionaryCopy = country.NameTranslationsDictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
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

            dictionaryCopy.Add("en", new CountryNameTranslationInfo
            {
                CommonName = country.Name,
                OfficialName = country.Name,
                Language = "English",
            });

            country.NameTranslationsDictionary = dictionaryCopy;
        }
    }

    private void EnrichWithMappingData(CountryInfo[] countries)
    {
        var fileContent = _fileStorageService.GetFileContentFromSource(DataFolderPath, "static-iso2fips.json");
        var isoAlpha2ToFipsMapping = JsonSerializer.Deserialize<Dictionary<string, string>>(fileContent);

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

    private static void FixData(CountryInfo[] countries)
    {
        // Edit germany
        var germany = countries.First(c => c.Code.Equals("DE"));

        if (germany is not null && germany.VehicleInfo is not null)
        {
            germany.VehicleInfo.InternationalRegistrationCodes = new string[] { "D" };
        }
    }

    private static void MapLatLongIntoGeoCoordinates(CountryInfo country)
    {
        if (country.LatLong?.Count == 2)
        {
            country.GeoCoordinates.Latitude = country.LatLong[0];
            country.GeoCoordinates.Longitude = country.LatLong[1];
        }
        country.LatLong = null!;

        if (country.CapitalInfo?.LatLong?.Count == 2)
        {
            country.CapitalInfo.GeoCoordinates.Latitude = country.CapitalInfo.LatLong[0];
            country.CapitalInfo.GeoCoordinates.Longitude = country.CapitalInfo.LatLong[1];
        }

        if (country.CapitalInfo != null)
        {
            country.CapitalInfo.LatLong = null!;
        }
    }

    public List<LanguageInfoYaml> GetLanguageIso639_3_Data(IConfiguration configuration)
    {
        var uriRestLanguages = configuration.GetValue<string>(ConfigurationConstants.UriLanguagesISO639)!;

        var data = RestHelper.GetInternetContent(uriRestLanguages).Content!;

        _fileStorageService.SaveContentToSource(data, DataFolderPath, "languages.yml");

        // Remove starting part
        data = data.Replace("---\n", string.Empty);

        var serializer = new Deserializer();
        var splitter = "- :name:";
        var splitContent = data.Split(splitter)[1..];

        var languages = new List<LanguageInfoYaml>();

        foreach (var splitPart in splitContent)
        {
            var dataPiece = splitPart;
            var encodeQuotes = false;

            // Handle case when quote is first character
            if (dataPiece.Contains('!'))
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