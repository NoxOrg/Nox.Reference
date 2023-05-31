using AutoMapper;

namespace Nox.Reference.Data.World.Mappings.Countries;

internal class CountrySingleMapping : ITypeConverter<string, Country>
{
    private readonly WorldDbContext _worldDbContext;

    public CountrySingleMapping(
        WorldDbContext worldDbContext)
    {
        _worldDbContext = worldDbContext;
    }

    public Country Convert(string source, Country destination, ResolutionContext context)
    {
        var country = _worldDbContext.Countries.FirstOrDefault(x => x.AlphaCode2 == source);

        if (country == null)
        {
            country = _worldDbContext.Countries.FirstOrDefault(x => x.AlphaCode3 == source);
        }
#pragma warning disable CS8603 // Possible null reference return. Convert is interface method.
        return country;
#pragma warning restore CS8603 // Possible null reference return.
    }
}