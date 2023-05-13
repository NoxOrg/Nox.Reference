using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using Nox.Reference.Data.Common.Seeds;
using System.Text.Json;

namespace Nox.Reference.Data.World;

internal class HolidayDataSeeder : NoxReferenceDataSeederBase<WorldDbContext, ICountryHolidayInfo, CountryHoliday>
{
    private readonly int[] _availableYears = new[] { 2023, 2024 };
    private readonly IConfiguration _configuration;

    public HolidayDataSeeder(
        IConfiguration configuration,
        WorldDbContext dbContext,
        IMapper mapper,
        ILogger<HolidayDataSeeder> logger,
        NoxReferenceFileStorageService fileStorageService)
        : base(dbContext, mapper, logger, fileStorageService)
    {
        _configuration = configuration;
    }

    public override string DataFolderPath => "Holidays";

    public override string TargetFileName => "Nox.Reference.Holidays.json";

    protected override List<ICountryHolidayInfo> GetDataInfos()
    {
        var holidaysZipPath = _configuration.GetValue<string>(ConfigurationConstants.HolidaysZipPathSettingName)!;
        var holidays = new List<ICountryHolidayInfo>();

        foreach (var year in _availableYears)
        {
            var filePath = Path.Combine(holidaysZipPath, $"Nox.Reference.Holidays-{year}.json.gz");

            var decompressedContent = FileUtilities.DecompressGzip(filePath);

            var holidayInfos = JsonSerializer.Deserialize<CountryHolidayInfo[]>(decompressedContent,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }) ?? Array.Empty<CountryHolidayInfo>();
            holidays.AddRange(holidayInfos);
        }

        return holidays;
    }
}