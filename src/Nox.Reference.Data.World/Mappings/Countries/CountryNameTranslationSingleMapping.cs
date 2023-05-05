using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryNameTranslationSingleMapping : ITypeConverter<INativeNameInfo, CountryNameTranslation>
{
    private readonly WorldDbContext _worldDbContext;
    private readonly ILogger<CountryNameTranslationSingleMapping> _logger;

    public CountryNameTranslationSingleMapping(
        WorldDbContext worldDbContext,
        ILogger<CountryNameTranslationSingleMapping> logger)
    {
        _worldDbContext = worldDbContext;
        _logger = logger;
    }

    public CountryNameTranslation Convert(INativeNameInfo source, CountryNameTranslation destination, ResolutionContext context)
    {
        var language = _worldDbContext
            .Set<Language>()
            .FirstOrDefault(x => x.Iso_639_1 == source.Language);

        if (language == null)
        {
            return null;
        }

        destination = new CountryNameTranslation
        {
            Language = language,
            CommonName = source.CommonName,
            OfficialName = source.OfficialName
        };

        return destination;
    }
}
