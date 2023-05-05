using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings;

internal class CountryNativeNameSingleMapping : ITypeConverter<INativeNameInfo, CountryNativeName>
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

    public CountryNativeName Convert(INativeNameInfo source, CountryNativeName destination, ResolutionContext context)
    {
        var language = _worldDbContext
            .Set<Language>()
            .FirstOrDefault(x => x.Iso_639_3 == source.Language);

        if (language == null)
        {
            return null;
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
