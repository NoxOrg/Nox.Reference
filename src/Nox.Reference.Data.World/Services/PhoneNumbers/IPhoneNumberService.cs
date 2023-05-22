using Nox.Reference.Data.World.Models;

namespace Nox.Reference.PhoneNumbers;

public interface IPhoneNumberService
{
    PhoneNumberInfo GetPhoneNumberInfo(string inputPhoneNumber, string? countryAlpha2Code = null);
}