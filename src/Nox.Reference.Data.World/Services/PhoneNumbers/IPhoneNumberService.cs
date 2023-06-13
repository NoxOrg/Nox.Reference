namespace Nox.Reference;

public interface IPhoneNumberService
{
    /// <summary>
    /// Get full phone number info by phone number
    /// </summary>
    /// <param name="inputPhoneNumber">Phone number string. Example: '+380965370000'</param>
    /// <param name="countryAlpha2Code">Country alpha 2 code</param>
    /// <returns>Phone number info</returns>
    PhoneNumberInfo GetPhoneNumberInfo(string inputPhoneNumber, string? countryAlpha2Code = null);
}