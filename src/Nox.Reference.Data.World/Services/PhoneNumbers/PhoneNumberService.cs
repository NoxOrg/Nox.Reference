using Microsoft.EntityFrameworkCore;
using Nox.Reference.Abstractions;
using Nox.Reference.Data.World;
using LibPhoneNumber = PhoneNumbers;

namespace Nox.Reference.PhoneNumbers;

internal class PhoneNumberService : IPhoneNumberService
{
    private readonly LibPhoneNumber.PhoneNumberUtil _phoneUtil = LibPhoneNumber.PhoneNumberUtil.GetInstance();
    private readonly LibPhoneNumber.PhoneNumberOfflineGeocoder _geoCoder = LibPhoneNumber.PhoneNumberOfflineGeocoder.GetInstance();
    private readonly WorldDbContext _worldDbContext;
    private Tuple<int, string>[] _phoneNumbers = new Tuple<int, string>[0];

    public PhoneNumberService(WorldDbContext worldDbContext)
    {
        _worldDbContext = worldDbContext;
    }

    public IPhoneNumberInfo GetPhoneNumberInfo(string inputPhoneNumber, string? countryAlpha2Code = null)
    {
        var phoneNumber = _phoneUtil.Parse(inputPhoneNumber, countryAlpha2Code);

        var isValid = _phoneUtil.IsValidNumber(phoneNumber);
        if (!isValid)
        {
            return new PhoneNumberInfo
            {
                InputNumber = inputPhoneNumber,
                IsValid = false
            };
        }
        var formattedNumber = _phoneUtil.Format(phoneNumber, LibPhoneNumber.PhoneNumberFormat.E164);
        var numberType = _phoneUtil.GetNumberType(phoneNumber).ToString() ?? "";

        var phoneInfo = new PhoneNumberInfo
        {
            InputNumber = inputPhoneNumber,
            IsValid = true,
            FormattedNumber = formattedNumber,
            NumberType = numberType,
            FormattedNumberNational = _phoneUtil.Format(phoneNumber, LibPhoneNumber.PhoneNumberFormat.NATIONAL),
            FormattedNumberInternational = _phoneUtil.Format(phoneNumber, LibPhoneNumber.PhoneNumberFormat.INTERNATIONAL),
            FormattedNumberRfc3966 = _phoneUtil.Format(phoneNumber, LibPhoneNumber.PhoneNumberFormat.RFC3966),
            RegionCode = _phoneUtil.GetRegionCodeForNumber(phoneNumber),
            RegionName = _geoCoder.GetDescriptionForNumber(phoneNumber, LibPhoneNumber.Locale.English),
            CarrierName = GuessCarrier(formattedNumber),
            IsMobile = !string.IsNullOrEmpty(numberType) && numberType.Equals("MOBILE")
        };

        return phoneInfo;
    }

    private string GuessCarrier(string formattedNumber)
    {
        var key = Convert.ToInt32(formattedNumber.TrimStart('+').PadRight(9, '0')[..9]);

        var phoneNumbers = GetPhoneNumbers();
        int min = 0;
        int max = phoneNumbers.Length - 1;
        int mid;

        while (min <= max)
        {
            mid = (min + max) / 2;
            if (key == phoneNumbers[mid].Item1)
            {
                max = mid;
                break;
            }
            else if (key < phoneNumbers[mid].Item1)
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }

        if (min < 1 || max > phoneNumbers.Length - 1)
        {
            return "Unknown";
        }

        return phoneNumbers[max].Item2;
    }

    private Tuple<int, string>[] GetPhoneNumbers()
    {
        if (_phoneNumbers.Any())
        {
            return _phoneNumbers;
        }

        _phoneNumbers = _worldDbContext
            .Set<CarrierPhoneNumber>()
            .Include(x => x.PhoneCarrier)
            .OrderBy(x => x.PhoneNumber)
            .Select(x => Tuple.Create(x.PhoneNumber, x.PhoneCarrier.Name))
            .ToArray();

        return _phoneNumbers;
    }
}