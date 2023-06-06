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

        return country!;
    }
}