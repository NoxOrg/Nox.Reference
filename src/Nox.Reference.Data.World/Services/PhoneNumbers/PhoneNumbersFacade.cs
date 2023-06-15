using Nox.Reference.Data.World;

namespace Nox.Reference;

public class PhoneNumbersFacade
{
    private readonly IWorldInfoContext _worldInfoContext;
    private readonly PhoneNumberService _phoneNumberService;

    public PhoneNumbersFacade(IWorldInfoContext worldInfoContext)
    {
        _worldInfoContext = worldInfoContext;
        _phoneNumberService = new PhoneNumberService(worldInfoContext);
    }

    public IQueryable<PhoneCarrier> PhoneCarriers => _worldInfoContext.PhoneCarriers;

    public PhoneNumberInfo GetPhoneNumberInfo(string inputPhoneNumber, string? countryAlpha2Code = null)
    {
        return _phoneNumberService.GetPhoneNumberInfo(inputPhoneNumber, countryAlpha2Code);
    }
}