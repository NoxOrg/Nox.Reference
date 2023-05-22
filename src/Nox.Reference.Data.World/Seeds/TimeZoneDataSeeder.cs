using AutoMapper;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;
using System.Text.RegularExpressions;
using static Nox.Reference.Data.World.Seeds.Utils.DataSeederUtils;

namespace Nox.Reference.Data.World;

internal class TimeZoneDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, Models.TimeZoneInfo, TimeZone>
{
    private readonly IConfiguration _configuration;

    private readonly Regex _scriptRegex = new Regex(@"<script.*/script>", RegexOptions.Singleline);

    public TimeZoneDataSeeder(
        IConfiguration configuration,
        WorldDbContext dbContext,
        IMapper mapper,
        ILogger<TimeZoneDataSeeder> logger,
        NoxReferenceFileStorageService fileStorageService) : base(dbContext, mapper, logger, fileStorageService)
    {
        _configuration = configuration;
    }

    public override string TargetFileName => "Nox.Reference.TimeZones.json";

    public override string DataFolderPath => "TimeZones";

    protected override List<Models.TimeZoneInfo> GetDataInfos()
    {
        var sourceOutputPath = _configuration.GetValue<string>(ConfigurationConstants.SourceDataPathSettingName)!;
        var timeZoneUrl = _configuration.GetValue<string>(ConfigurationConstants.TimeZoneUrl)!;
        var nodeTimeUrl = _configuration.GetValue<string>(ConfigurationConstants.NodaTimeUrl)!;

        var sourceFilePath = Path.Combine(sourceOutputPath, "TimeZones");
        Directory.CreateDirectory(sourceFilePath);

        var timeZoneDataToSave = new List<Models.TimeZoneInfo>();
        var htmlWeb = new HtmlWeb();
        var htmlDoc = htmlWeb.Load(timeZoneUrl);

        // Save content
        var body = htmlDoc.DocumentNode.SelectSingleNode("/html/body").OuterHtml;
        var formattedBody = _scriptRegex.Replace(body, string.Empty);
        File.WriteAllText(Path.Combine(sourceFilePath, "wiki.html"), formattedBody);

        var wikiTimeZoneRows = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"mw-content-text\"]/div[1]/table[1]/tbody/tr");

        for (var nodeIndex = 0; nodeIndex < wikiTimeZoneRows!.Count - 2; nodeIndex++)
        {
            try
            {
                var cells = htmlDoc.DocumentNode.SelectNodes($"//*[@id=\"mw-content-text\"]/div[1]/table[1]/tbody/tr[{nodeIndex + 3}]/td");
                var countryCell = cells[0];
                var tzIdCell = cells[1];
                var embeddedCommentsCell = cells[2];
                var typeCell = cells[3];
                var utcOffsetStdCell = cells[4];
                var utcOffsetDstCell = cells[5];
                var stdTimeZoneAbbreviationCell = cells[6];

                var timeZone = new Models.TimeZoneInfo
                {
                    Id = GetNodeText(tzIdCell),
                    EmbeddedComments = GetNodeTextOrNull(embeddedCommentsCell),
                    Type = GetNodeText(typeCell).Replace("†", " (Obsolete)"),
                    SDT_UTC_Offset = GetNodeText(utcOffsetStdCell),
                    DST_UTC_Offset = GetNodeText(utcOffsetDstCell),
                    SDT_TimeZoneAbbreviation = GetNodeText(stdTimeZoneAbbreviationCell),
                    CountriesWithTimeZone = GetNodeTextOrNull(countryCell)?.Split(",").Select(x => x.Trim()).ToList() ?? new List<string>(),
                };

                var isDstCellPresent = cells.Count > 9;

                if (isDstCellPresent)
                {
                    var dstTimeZoneAbbreviationCell = cells[7];
                    var notesCell = cells[9];

                    timeZone.DST_TimeZoneAbbreviation = GetNodeText(dstTimeZoneAbbreviationCell);
                    timeZone.Notes = GetNodeTextOrNull(notesCell);
                }
                else
                {
                    var notesCell = cells[8];

                    timeZone.Notes = GetNodeTextOrNull(notesCell);
                }

                timeZoneDataToSave.Add(timeZone);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured during fetching data from index page. Does index page empty or changed? Error message: {Message}", ex.Message);
            }
        }

        htmlWeb = new HtmlWeb();
        htmlDoc = htmlWeb.Load(nodeTimeUrl);

        // Save content
        body = htmlDoc.DocumentNode.SelectSingleNode("/html/body").OuterHtml;
        formattedBody = _scriptRegex.Replace(body, string.Empty);
        File.WriteAllText(Path.Combine(sourceFilePath, "nodeTime.html"), formattedBody);

        var nodaTimeRows = htmlDoc.DocumentNode.SelectNodes("/html/body/section/div/div/table/tr");

        for (var nodeIndex = 1; nodeIndex < nodaTimeRows!.Count; nodeIndex++)
        {
            try
            {
                var cells = htmlDoc.DocumentNode.SelectNodes($"/html/body/section/div/div/table/tr[{nodeIndex + 1}]/td");
                var countryCell = cells[0];
                var latLongCell = cells[5];

                var timeZone = timeZoneDataToSave.FirstOrDefault(x => x.Id.Equals(GetNodeText(countryCell)));
                if (timeZone == null)
                {
                    continue;
                }

                var latLongContent = GetNodeTextOrNull(latLongCell);
                if (latLongContent == null)
                {
                    continue;
                }

                var splitLatLong = latLongContent.TrimStart('(').TrimEnd(')').Split(",&nbsp;");
                timeZone.GeoCoordinates = new GeoCoordinatesInfo
                {
                    Latitude = decimal.Parse(splitLatLong[0], System.Globalization.CultureInfo.InvariantCulture),
                    Longitude = decimal.Parse(splitLatLong[1], System.Globalization.CultureInfo.InvariantCulture)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured during fetching data from Node Time page. Does index page empty or changed? Error message: {Message}", ex.Message);
            }
        }

        return timeZoneDataToSave;
    }
    protected override void DoSpecialTreatAfterAdding(IEnumerable<Models.TimeZoneInfo> sources, IEnumerable<TimeZone> destinations)
    {
        base.DoSpecialTreatAfterAdding(sources, destinations);

        var countries = _dbContext.Set<Country>().ToList();

        foreach (var source in sources)
        {
            var timeZoneEntity = destinations.First(x => x.Code == source.Id);
            timeZoneEntity.Countries = countries.Where(x => source.CountriesWithTimeZone.Contains(x.Code)).ToList();
        }

        _dbContext.Set<TimeZone>()
            .UpdateRange(destinations);

        _dbContext.SaveChanges();
    }
}