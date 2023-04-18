using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;
using Nox.Reference.VatNumbers.Services.Validators;
using System.Collections.Concurrent;

namespace Nox.Reference.Holidays;

public class VatNumberService : IVatNumberService
{
    private static readonly ConcurrentDictionary<string, VatNumberInfo> _vatNumberFormatsByCountry = new ConcurrentDictionary<string, VatNumberInfo>();

    public static void Init(IEnumerable<VatNumberInfo> vatNumberInfos)
    {
        foreach (var vatNumberInfo in vatNumberInfos)
        {
            _vatNumberFormatsByCountry[vatNumberInfo.Country] = vatNumberInfo;
        }
    }

    public VatNumberService() {  }

    public IVatNumberInfo GetCountryVatInfo(IVatNumberInfo vatNumberInfo)
    {
        // Ensure that even in null case non-null value with an error is returned
        if (string.IsNullOrWhiteSpace(vatNumberInfo.Country))
        {
            var result = new VatNumberInfo(vatNumberInfo);
            result.ValidationResult.ValidationErrors.Add(ValidationErrors.EmptyCountryError);
            return result;
        }

        var enrichedVatInfo = new VatNumberInfo(vatNumberInfo);

        // Copy country data from list if possible
        if (_vatNumberFormatsByCountry.TryGetValue(vatNumberInfo.Country, out var countryVatInfo))
        {
            enrichedVatInfo = new VatNumberInfo(countryVatInfo);
            enrichedVatInfo.OriginalVatNumber = vatNumberInfo.OriginalVatNumber;
            enrichedVatInfo.Country = vatNumberInfo.Country;
        }

        return enrichedVatInfo;
    }

    public IVatNumberInfo ValidateVatNumber(IVatNumberInfo vatNumberInfo, bool shouldValidateViaApi = true)
    {
        var enrichedVatInfo = GetCountryVatInfo(vatNumberInfo);

        if (string.IsNullOrWhiteSpace(enrichedVatInfo.OriginalVatNumber))
        {
            enrichedVatInfo.ValidationResult.ValidationErrors.Add(ValidationErrors.EmptyVatNumberError);
        }

        if (!enrichedVatInfo.ValidationResult.IsValid)
        {
            return enrichedVatInfo;
        }

        enrichedVatInfo.FormattedVatNumber = enrichedVatInfo.OriginalVatNumber.NormalizeVatNumber(enrichedVatInfo.Country);

        if (_vatNumberFormatsByCountry.ContainsKey(enrichedVatInfo.Country) ||
            VatValidationService.IsSupportingCountryValidation(enrichedVatInfo.Country))
        {
            enrichedVatInfo.IsVerified = true;
            enrichedVatInfo.ValidationResult = VatValidationService.ValidateVatNumber(enrichedVatInfo, shouldValidateViaApi);
        }

        return enrichedVatInfo;
    }
}
