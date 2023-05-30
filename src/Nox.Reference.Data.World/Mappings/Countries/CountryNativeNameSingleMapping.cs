using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryNativeNameSingleMapping : ITypeConverter<CountryNameTranslationInfo, CountryNativeName>
{
    private readonly WorldDbContext _worldDbContext;
    private readonly ILogger<CountryNativeNameSingleMapping> _logger;

    public CountryNativeNameSingleMapping(
        WorldDbContext worldDbContext,
        ILogger<CountryNativeNameSingleMapping> logger)
    {
        _worldDbContext = worldDbContext;
        _logger = logger;
    }

    public CountryNativeName Convert(CountryNameTranslationInfo source, CountryNativeName destination, ResolutionContext context)
    {
        var language = _worldDbContext
            .Set<Language>()
            .FirstOrDefault(x => x.Iso_639_3 == source.Language);

        if (language == null)
        {
            _logger.LogWarning("Language {lang} has not found.", source.Language);
#pragma warning disable CS8603 // Possible null reference return. Method Convert is interface method. It gets the same error if it's marked as nullable
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        destination = new CountryNativeName
        {
            Language = language,
            CommonName = source.CommonName,
            OfficialName = source.OfficialName
        };

        return destination;
    }
}