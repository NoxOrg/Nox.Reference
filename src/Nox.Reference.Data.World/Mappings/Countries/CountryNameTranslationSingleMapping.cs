using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryNameTranslationSingleMapping : ITypeConverter<CountryNameTranslationInfo, CountryNameTranslation>
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

    public CountryNameTranslation Convert(CountryNameTranslationInfo source, CountryNameTranslation destination, ResolutionContext context)
    {
        var language = _worldDbContext
            .Set<Language>()
            .FirstOrDefault(x => x.Iso_639_1 == source.Language);

        if (language == null)
        {
            _logger.LogWarning("Language {lang} has not found.", source.Language);
#pragma warning disable CS8603 // Possible null reference return. Method Convert is interface method. It gets the same error if it's marked as nullable
            return null;
#pragma warning restore CS8603 // Possible null reference return.
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