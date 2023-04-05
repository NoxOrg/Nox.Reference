using System.Reflection;
using System.Text.Json;

namespace Nox.Reference.Countries;

public class CountriesService : ICountriesService
{
    private readonly IReadOnlyList<ICountryInfo> _countries = new List<ICountryInfo>();

    public CountriesService()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Nox.Reference.Countries.json";
        if (assembly == null)
        {
            return;
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return;
        }

        using var reader = new StreamReader(stream);

        _countries = JsonSerializer.Deserialize<List<CountryInfo>>(
            reader.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new List<CountryInfo>();
    }

    public IReadOnlyList<ICountryInfo> GetCountries() => _countries;
}