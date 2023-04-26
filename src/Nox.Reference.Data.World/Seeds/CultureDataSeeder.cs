using AutoMapper;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common;
using Nox.Reference.Data.World.Entities.Cultures;
using Nox.Reference.Data.World.Models.Cultures;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Nox.Reference.Data.World;

internal class CultureDataSeeder : INoxReferenceDataSeeder
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly WorldDbContext _dbContext;
    private readonly ILogger<CultureDataSeeder> _logger;

    private readonly Regex _scriptRegex = new Regex(@"<script.*/script>", RegexOptions.Singleline);

    public string TargetFileName => "Nox.Reference.Cultures.json";

    public CultureDataSeeder(
        IConfiguration configuration,
        IMapper mapper,
        WorldDbContext dbContext,
        ILogger<CultureDataSeeder> logger)
    {
        _configuration = configuration;
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }

    public void Seed()
    {
        _logger.LogInformation("Getting culture data...");

        var dataSet = _dbContext
           .Set<Culture>();

        if (dataSet.Any())
        {
           _logger.LogInformation("Data set is not empty. Finishing operation.");
           return;
        }

        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var targetOutputPath = _configuration.GetValue<string>(ConfigurationConstants.TargetDataPathSettingName)!;
        var uriLocalePlanetList = _configuration.GetValue<string>(ConfigurationConstants.UriLocalePlanetList)!;
        var uriLocalePlanetItem = _configuration.GetValue<string>(ConfigurationConstants.UriLocalePlanetItem)!;

        try
        {
            var sourceFilePath = Path.Combine(sourceOutputPath, "Cultures");
            Directory.CreateDirectory(sourceFilePath);

            var targetFilePath = targetOutputPath;
            Directory.CreateDirectory(targetFilePath);

            var culturesDataToSave = new List<CultureInfo>();
            var htmlWeb = new HtmlWeb();
            var htmlDoc = htmlWeb.Load(uriLocalePlanetList);

            // Save content
            var body = htmlDoc.DocumentNode.SelectSingleNode("/html/body").OuterHtml;
            var formattedBody = _scriptRegex.Replace(body, string.Empty);
            File.WriteAllText(Path.Combine(sourceFilePath, "localePlanetList.html"), formattedBody);

            var nodes = htmlDoc.DocumentNode.SelectNodes("/html/body/div[2]/div/table/tbody/tr/td");

            try
            {
                for (var nodeIndex = 0; nodeIndex < nodes!.Count; nodeIndex += 4)
                {
                    var idNode = nodes[nodeIndex]!;
                    var formalNameNode = nodes[nodeIndex + 1]!;
                    var nativeNameNode = nodes[nodeIndex + 2]!;
                    var commonNameNode = nodes[nodeIndex + 3]!;

                    var cultureInfo = new CultureInfo
                    {
                        Id = GetNodeText(idNode).Replace('_', '-'),
                        FormalName = GetNodeText(formalNameNode),
                        NativeName = GetNodeText(nativeNameNode),
                        CommonName = GetNodeTextOrNull(commonNameNode)
                    };
    
                    culturesDataToSave.Add(cultureInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured during fetching data from index page. Does index page empty or changed? Error message: {Message}", ex.Message);
            }

            foreach (var cultureInfo in culturesDataToSave)
            {
                var uri = uriLocalePlanetItem.Replace(ConfigurationConstants.LocaleCodePlaceholder, cultureInfo.Id);

                htmlDoc = htmlWeb.Load(uri);
                nodes = htmlDoc.DocumentNode.SelectNodes("/html/body/div[2]/div/table/tr/td");

                // Save content
                body = htmlDoc.DocumentNode.SelectSingleNode("/html/body").OuterHtml;
                formattedBody = _scriptRegex.Replace(body, string.Empty);
                File.WriteAllText(Path.Combine(sourceFilePath, $"localePlanetItem_{cultureInfo.Id}.html"), formattedBody);

                var languageNode = nodes[3];
                var countryNode = nodes[5];
                var displayNameNode = nodes[7];
                var displayNameWithDialectNode = nodes[9];
                var characterOrientationNode = nodes[11];
                var lineOrientationNode = nodes[13];
                var languageIso639_2t_node = nodes[15];

                cultureInfo.Language = GetNodeText(languageNode);
                cultureInfo.Country = GetNodeText(countryNode);
                cultureInfo.DisplayName = GetNodeText(displayNameNode);
                cultureInfo.DisplayNameWithDialect = GetNodeText(displayNameWithDialectNode);
                cultureInfo.CharacterOrientation = GetNodeText(characterOrientationNode);
                cultureInfo.LineOrientation = GetNodeText(lineOrientationNode);
                cultureInfo.LanguageIso_639_2t = GetNodeTextOrNull(languageIso639_2t_node);

                var currencySymbolNode = nodes[21];
                var decimalSeparatorNode = nodes[23];
                var digitNode = nodes[25];
                var exponentSeparatorNode = nodes[27];
                var groupingSeparatorNode = nodes[29];
                var infinityNode = nodes[31];
                var internationalCurrencySymbolNode = nodes[33];
                var minusSignNode = nodes[35];
                var monetaryDecimalSeparatorNode = nodes[37];
                var notANumberNode = nodes[39];
                var PadEscapeNode = nodes[41];
                var PatternSeparatorNode = nodes[43];
                var PercentNode = nodes[45];
                var PerMillNode = nodes[47];
                var PlusSignNode = nodes[49];
                var SignificantDigitNode = nodes[51];
                var ZeroDigitNode = nodes[53];

                var numberFormat = new NumberFormatInfo()
                {
                    CurrencySymbol = GetNodeText(currencySymbolNode),
                    DecimalSeparator = GetNodeText(decimalSeparatorNode),
                    Digit = GetNodeText(digitNode),
                    ExponentSeparator = GetNodeText(exponentSeparatorNode),
                    GroupingSeparator = GetNodeText(groupingSeparatorNode),
                    Infinity = GetNodeText(infinityNode),
                    InternationalCurrencySymbol = GetNodeText(internationalCurrencySymbolNode),
                    MinusSign = GetNodeText(minusSignNode),
                    MonetaryDecimalSeparator = GetNodeText(monetaryDecimalSeparatorNode),
                    NotANumberSymbol = GetNodeText(notANumberNode),
                    PadEscape = GetNodeText(PadEscapeNode),
                    PatternSeparator = GetNodeText(PatternSeparatorNode),
                    Percent = GetNodeText(PercentNode),
                    PerMill = GetNodeText(PerMillNode),
                    PlusSign = GetNodeText(PlusSignNode),
                    SignificantDigit = GetNodeText(SignificantDigitNode),
                    ZeroDigit = GetNodeText(ZeroDigitNode),
                };
                cultureInfo.NumberFormat = numberFormat;

                var amPsStringNode = nodes[55];
                var erasNode = nodes[57];
                var eraNamesNode = nodes[59];
                var monthNode= nodes[61];
                var shortMonthsNode = nodes[63];
                var shortWeekdaysNode = nodes[65];
                var weekdaysNode = nodes[67];
                var date3Node = nodes[69];
                var date2Node = nodes[71];
                var date1Node = nodes[73];
                var date0Node = nodes[75];

                var dateFormat = new DateFormatInfo()
                {
                    AmPmStrings = GetNodeText(amPsStringNode),
                    Eras = GetNodeText(erasNode),
                    EraNames = GetNodeText(eraNamesNode),
                    Months = GetNodeText(monthNode),
                    ShortMonths = GetNodeText(shortMonthsNode),
                    ShortWeekdays = GetNodeText(shortWeekdaysNode),
                    Weekdays = GetNodeText(weekdaysNode),
                    Date_3 = GetDateNode(date3Node),
                    Date_2 = GetDateNode(date2Node),
                    Date_1 = GetDateNode(date1Node),
                    Date_0 = GetDateNode(date0Node),
                };

                cultureInfo.DateFormat = dateFormat;

                // When web scraping it's ethical to add a timeout
                Task.Delay(500).Wait();
            }

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            // Map yaml model to normal model
            var outputContent = JsonSerializer.Serialize(culturesDataToSave, options);

            File.WriteAllText(Path.Combine(targetFilePath, TargetFileName), outputContent);

            var entities = _mapper.Map<List<Culture>>(culturesDataToSave);

            dataSet.AddRange(entities);

            _dbContext.SaveChanges();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    private string GetNodeText(HtmlNode? htmlNode)
    {
        var value = htmlNode?.InnerText;
        if (string.IsNullOrWhiteSpace(value))
        {
#pragma warning disable S112 // General exceptions should never be thrown
            throw new Exception("Error! Null value was encountered on not nullable node.");
#pragma warning restore S112 // General exceptions should never be thrown
        }

        return value.Trim();
    }

    private string? GetNodeTextOrNull(HtmlNode? htmlNode)
    {
        var value = htmlNode?.InnerText;
        if (string.IsNullOrWhiteSpace(value))
        {
            value = null;
        }

        return value?.Trim();
    }

    private string GetDateNode(HtmlNode? htmlNode)
    {
        var value = htmlNode?.InnerHtml?.Replace("<br>", Environment.NewLine);
        if (string.IsNullOrWhiteSpace(value))
        {
            value = null;
        }

        return value?.Trim();
    }
}