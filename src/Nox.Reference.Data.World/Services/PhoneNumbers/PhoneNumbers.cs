using Nox.Reference.Data.World.Models;
using Nox.Reference.PhoneNumbers;

namespace Nox.Reference.Data.World.Services.PhoneNumbers
{
    public class PhoneNumbers
    {
        public IWorldInfoContext _worldInfoContext { get; set; }
        public PhoneNumberService _phoneNumberService { get; set; }

        public PhoneNumbers(IWorldInfoContext worldInfoContext)
        {
            _worldInfoContext = worldInfoContext;
            _phoneNumberService = new PhoneNumberService(worldInfoContext);

        }

        public IQueryable<CarrierPhoneNumber> CarrierPhoneNumbers => _worldInfoContext.CarrierPhoneNumbers;

        public PhoneNumberInfo GetPhoneNumberInfo(string inputPhoneNumber, string? countryAlpha2Code = null)
        {
            return _phoneNumberService.GetPhoneNumberInfo(inputPhoneNumber, countryAlpha2Code);
        }
    }
}
