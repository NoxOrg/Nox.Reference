
using System.Reflection;
using System.Text.Json;

namespace Nox.Reference.Countries;

public class CountriesService : ICountriesService
{
    private readonly ICountryInfo[]? _countries;

    public CountriesService()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Nox.Reference.Countries.json";
        if (assembly != null)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using var reader = new StreamReader(stream);

                _countries = JsonSerializer.Deserialize<CountryInfo[]>(reader.ReadToEnd(),
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
        }
    }

    public ICountryInfo[]? GetCountries() => _countries;
}
