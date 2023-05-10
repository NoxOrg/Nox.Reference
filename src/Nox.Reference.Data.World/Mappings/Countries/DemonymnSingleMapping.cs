using AutoMapper;
using Microsoft.Extensions.Logging;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings;

internal class DemonymnSingleMapping : ITypeConverter<IDemonymn, Demonymn>
{
    private readonly WorldDbContext _worldDbContext;
    private readonly ILogger<DemonymnSingleMapping> _logger;

    public DemonymnSingleMapping(
        WorldDbContext worldDbContext,
        ILogger<DemonymnSingleMapping> logger)
    {
        _worldDbContext = worldDbContext;
        _logger = logger;
    }

    public Demonymn Convert(IDemonymn source, Demonymn destination, ResolutionContext context)
    {
        var language = _worldDbContext
            .Set<Language>()
            .FirstOrDefault(x => x.Iso_639_3 == source.Language);

        if (language == null)
        {
#pragma warning disable CS8603 // Possible null reference return. Method Convert is interface method. It gets the same error if it's marked as nullable
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        destination = new Demonymn
        {
            Language = language,
            Feminine = source.Feminine,
            Masculine = source.Masculine
        };

        return destination;
    }
}