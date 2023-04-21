using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace Nox.Reference.Data.World;

public class LanguageDataSeed : INoxReferenceDataSeeder
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<LanguageDataSeed> _logger;

    public LanguageDataSeed(
        IConfiguration configuration,
        ILogger<LanguageDataSeed> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public void Seed()
    {
        _logger.LogInformation("Getting language data...");

        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;
        var uriRestLanguagesAdditionalInfo = _configuration.GetValue<string>(ConfigurationConstants.UriLanguagesAdditionalInfo)!;
        try
        {
            var sourceFilePath = Path.Combine(sourceOutputPath, "Languages");
            Directory.CreateDirectory(sourceFilePath);

            var targetFilePath = targetOutputPath;
            Directory.CreateDirectory(targetFilePath);

            var languages = GetLanguageIso639_3_Data(_configuration, sourceFilePath);

            var languagesToSave = languages
                .Select(x => x.ToLanguageInfo())
                .ToList();

            EnrichWithEnglishTranslation(languagesToSave);
            EnrichAdditionalData(uriRestLanguagesAdditionalInfo, languagesToSave);
            EnrichLanguageDataWithNativeNames(languagesToSave, sourceFilePath);

            // Store output
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            // Map yaml model to normal model
            var outputContent = JsonSerializer.Serialize(languagesToSave, options);

            File.WriteAllText(Path.Combine(targetFilePath, "Nox.Reference.Languages.json"), outputContent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    private static void EnrichWithEnglishTranslation(List<LanguageInfo> languagesToSave)
    {
        foreach (var language in languagesToSave)
        {
            if (language.Name == "English")
            {
                continue;
            }

            language.NameTranslations_.Add(new LanguageTranslation
            {
                Language = "en",
                Translation = language.Name,
            });
        }
    }

    private void EnrichLanguageDataWithNativeNames(List<LanguageInfo> languagesToSave, string sourcePath)
    {
        using var reader = new StreamReader(Path.Combine(sourcePath, "native_language_names.json"));
        var deserializationOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        var nativeLanguageNamesInfo = JsonSerializer.Deserialize<List<LanguageNativeNameInfo>>(reader.ReadToEnd(), deserializationOptions) ?? new();
        foreach (var nativeLaguageInfo in nativeLanguageNamesInfo)
        {
            var language = languagesToSave.FirstOrDefault(x => nativeLaguageInfo.Code.Equals(x.Iso_639_1));
            if (language != null)
            {
                language.NameTranslations_.Add(new LanguageTranslation
                {
                    Language = language.Iso_639_1!,
                    Translation = nativeLaguageInfo.NativeName
                });
            }
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

    private static void EnrichAdditionalData(string uriRestLanguagesAdditionalInfo, List<LanguageInfo> languagesToSave)
    {
        var additionalData = RestHelper.GetInternetContent(uriRestLanguagesAdditionalInfo).Content!;
        var deserializationOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        var additionalLanguageData = JsonSerializer.Deserialize<Dictionary<string, LanguageInfoAdditionalInfo>>(additionalData, deserializationOptions) ?? new();
        foreach (var addtionalInfoForCountry in additionalLanguageData.Values)
        {
            var language = languagesToSave.FirstOrDefault(x => addtionalInfoForCountry.Iso_639_2.Equals(x.Iso_639_2t));
            if (language != null)
            {
                language.WikiUrl_ = addtionalInfoForCountry.WikiUrl;
                language.NameTranslations_.Add(new LanguageTranslation
                {
                    Language = "fr",
                    Translation = addtionalInfoForCountry.FrenchName!.First()
                });
                language.NameTranslations_.Add(new LanguageTranslation
                {
                    Language = "de",
                    Translation = addtionalInfoForCountry.GermanName!.First()
                });
            }
        }
    }
}