namespace Nox.Reference.PhoneNumbers
{
    public interface IPhoneNumberService
    {
        /// <summary>
        /// Get phone number info
        /// </summary>
        /// <param name="inputPhoneNumber">Phone number</param>
        /// <param name="countryAlpha2Code">Iso 2 code for country</param>
        /// <returns>Full phone number info</returns>
        IPhoneNumberInfo GetPhoneNumberInfo(string inputPhoneNumber, string? countryAlpha2Code = null);
    }
}