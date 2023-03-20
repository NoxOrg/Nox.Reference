namespace Nox.Reference.Countries
{
    public interface ICountriesService
    {
        IReadOnlyList<ICountryInfo> GetCountries();
    }
}