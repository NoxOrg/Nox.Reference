using AutoMapper;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;
using Nox.Reference.Data.World.Entities.Cultures;
using Nox.Reference.Data.World.Models.Cultures;
using Nox.Reference.Data.World.Seeds.Utils;
using System.Text.RegularExpressions;

namespace Nox.Reference.Data.World;

internal class CultureDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, CultureInfo, Culture>
{
    private readonly IConfiguration _configuration;

    private readonly Regex _scriptRegex = new Regex(@"<script.*/script>", RegexOptions.Singleline);

    public CultureDataSeeder(
        IConfiguration configuration,
        WorldDbContext dbContext,
        IMapper mapper,
        ILogger<CultureDataSeeder> logger,
        NoxReferenceFileStorageService fileStorageService)
        : base(dbContext, mapper, logger, fileStorageService)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.Cultures.json";

    public override string DataFolderPath => "Cultures";

    protected override IEnumerable<CultureInfo> GetDataInfos()
    {
        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var uriLocalePlanetList = _configuration.GetValue<string>(ConfigurationConstants.UriLocalePlanetList)!;
        var uriLocalePlanetItem = _configuration.GetValue<string>(ConfigurationConstants.UriLocalePlanetItem)!;

        var sourceFilePath = Path.Combine(sourceOutputPath, "Cultures");
        Directory.CreateDirectory(sourceFilePath);

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
                    Id = DataSeederUtils.GetNodeText(idNode).Replace('_', '-'),
                    FormalName = DataSeederUtils.GetNodeText(formalNameNode),
                    NativeName = DataSeederUtils.GetNodeText(nativeNameNode),
                    CommonName = DataSeederUtils.GetNodeTextOrNull(commonNameNode)
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

            cultureInfo.Language = DataSeederUtils.GetNodeText(languageNode);
            cultureInfo.Country = DataSeederUtils.GetNodeText(countryNode);
            cultureInfo.DisplayName = DataSeederUtils.GetNodeText(displayNameNode);
            cultureInfo.DisplayNameWithDialect = DataSeederUtils.GetNodeText(displayNameWithDialectNode);
            cultureInfo.CharacterOrientation = DataSeederUtils.GetNodeText(characterOrientationNode);
            cultureInfo.LineOrientation = DataSeederUtils.GetNodeText(lineOrientationNode);
            cultureInfo.LanguageIso_639_2t = DataSeederUtils.GetNodeTextOrNull(languageIso639_2t_node);

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
                CurrencySymbol = DataSeederUtils.GetNodeText(currencySymbolNode),
                DecimalSeparator = DataSeederUtils.GetNodeText(decimalSeparatorNode),
                Digit = DataSeederUtils.GetNodeText(digitNode),
                ExponentSeparator = DataSeederUtils.GetNodeText(exponentSeparatorNode),
                GroupingSeparator = DataSeederUtils.GetNodeText(groupingSeparatorNode),
                Infinity = DataSeederUtils.GetNodeText(infinityNode),
                InternationalCurrencySymbol = DataSeederUtils.GetNodeText(internationalCurrencySymbolNode),
                MinusSign = DataSeederUtils.GetNodeText(minusSignNode),
                MonetaryDecimalSeparator = DataSeederUtils.GetNodeText(monetaryDecimalSeparatorNode),
                NotANumberSymbol = DataSeederUtils.GetNodeText(notANumberNode),
                PadEscape = DataSeederUtils.GetNodeText(PadEscapeNode),
                PatternSeparator = DataSeederUtils.GetNodeText(PatternSeparatorNode),
                Percent = DataSeederUtils.GetNodeText(PercentNode),
                PerMill = DataSeederUtils.GetNodeText(PerMillNode),
                PlusSign = DataSeederUtils.GetNodeText(PlusSignNode),
                SignificantDigit = DataSeederUtils.GetNodeText(SignificantDigitNode),
                ZeroDigit = DataSeederUtils.GetNodeText(ZeroDigitNode),
            };
            cultureInfo.NumberFormat = numberFormat;

            var amPsStringNode = nodes[55];
            var erasNode = nodes[57];
            var eraNamesNode = nodes[59];
            var monthNode = nodes[61];
            var shortMonthsNode = nodes[63];
            var shortWeekdaysNode = nodes[65];
            var weekdaysNode = nodes[67];
            var date3Node = nodes[69];
            var date2Node = nodes[71];
            var date1Node = nodes[73];
            var date0Node = nodes[75];

            var dateFormat = new DateFormatInfo()
            {
                AmPmStrings = DataSeederUtils.GetNodeText(amPsStringNode),
                Eras = DataSeederUtils.GetNodeText(erasNode),
                EraNames = DataSeederUtils.GetNodeText(eraNamesNode),
                Months = DataSeederUtils.GetNodeText(monthNode),
                ShortMonths = DataSeederUtils.GetNodeText(shortMonthsNode),
                ShortWeekdays = DataSeederUtils.GetNodeText(shortWeekdaysNode),
                Weekdays = DataSeederUtils.GetNodeText(weekdaysNode),
                Date_3 = DataSeederUtils.GetDateNode(date3Node),
                Date_2 = DataSeederUtils.GetDateNode(date2Node),
                Date_1 = DataSeederUtils.GetDateNode(date1Node),
                Date_0 = DataSeederUtils.GetDateNode(date0Node),
            };

            cultureInfo.DateFormat = dateFormat;

            // When web scraping it's ethical to add a timeout
            Task.Delay(500).Wait();
        }
        return culturesDataToSave;
    }
}