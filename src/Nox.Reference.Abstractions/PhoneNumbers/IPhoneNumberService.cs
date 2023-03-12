namespace Nox.Reference.PhoneNumbers
{
    public interface IPhoneNumberService
    {
        IPhoneNumberInfo GetPhoneNumberInfo(string inputPhoneNumber, string? countryAlpha2Code = null);
    }
}