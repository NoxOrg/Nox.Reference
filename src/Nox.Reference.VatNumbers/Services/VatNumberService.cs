using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using Nox.Reference.VatNumbers.Models;
using Nox.Reference.VatNumbers.Services.Validators;
using System.Reflection;
using System.Text.Json;

namespace Nox.Reference.Holidays;

public class VatNumberService : IVatNumberService
{
    private readonly Dictionary<string, VatNumberInfo> _vatNumberFormatsByCountry = new Dictionary<string, VatNumberInfo>();

    public VatNumberService()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"Nox.Reference.VatNumbers.json";
        if (assembly == null)
        {
            return;
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return;
        }

        using var reader = new StreamReader(stream);
        var countryInfo = JsonSerializer.Deserialize<List<VatNumberInfo>>(
            reader.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }) ?? new List<VatNumberInfo>();

        foreach (var country in countryInfo)
        {
            _vatNumberFormatsByCountry[country.CountryIso2Code] = country;
        }
    }

    public IVatNumberInfo GetCountryVatInfo(IVatNumberInfo vatNumberInfo)
    {
        // Ensure that even in null case non-null value with an error is returned
        if (string.IsNullOrWhiteSpace(vatNumberInfo.CountryIso2Code))
        {
            var result = new VatNumberInfo(vatNumberInfo);
            result.ValidationResult.ValidationErrors.Add(ValidationErrors.EmptyCountryError);
            return result;
        }

        var enrichedVatInfo = new VatNumberInfo(vatNumberInfo);

        // Copy country data from list if possible
        if (_vatNumberFormatsByCountry.TryGetValue(vatNumberInfo.CountryIso2Code, out var countryVatInfo))
        {
            enrichedVatInfo = new VatNumberInfo(countryVatInfo);
            enrichedVatInfo.OriginalVatNumber = vatNumberInfo.OriginalVatNumber;
            enrichedVatInfo.CountryIso2Code = vatNumberInfo.CountryIso2Code;
        }

        return enrichedVatInfo;
    }

    public IVatNumberInfo ValidateVatNumber(IVatNumberInfo vatNumberInfo)
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

        enrichedVatInfo.FormattedVatNumber = enrichedVatInfo.NormalizeVatNumber();
        // TODO: improve horrible piece
        if (_vatNumberFormatsByCountry.ContainsKey(enrichedVatInfo.CountryIso2Code) ||
            VatValidationService.IsSupportingCountryValidation(enrichedVatInfo.CountryIso2Code))
        {
            enrichedVatInfo.IsVerified = true;
            enrichedVatInfo.ValidationResult = VatValidationService.ValidateVatNumber(enrichedVatInfo);
        }

        return enrichedVatInfo;
    }
}
