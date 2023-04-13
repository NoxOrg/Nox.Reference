namespace Nox.Reference.Countries
{
    public interface ICountriesService
    {
        /// <summary>
        /// Get countries list
        /// </summary>
        /// <returns>IReadOnlyList with all supported countries and their info</returns>
        IReadOnlyList<ICountryInfo> GetCountries();
    }
}