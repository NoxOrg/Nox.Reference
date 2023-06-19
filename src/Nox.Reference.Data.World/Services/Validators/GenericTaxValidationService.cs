using Nox.Reference.Data.World.Helpers;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Nox.Reference.Data.World;

// TODO: join with vat number service into single service if possible
// Important: not production ready, should be tested and refined
public static class GenericTaxValidationService
{
    /// <summary>
    /// Validates tax number string and returns validation result
    /// </summary>
    /// <param name="taxNumber">Tax number as text</param>
    /// <param name="taxNumberInfo">Information that will be used for validation process</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static TaxNumberValidationResult ValidateTaxNumber(
        string taxNumber,
        TaxNumberDefinition taxNumberInfo,
        bool shouldValidateViaApi = true)
    {
        var result = TaxNumberValidationResult.CreateWithValidation(taxNumber, taxNumberInfo.Country.AlphaCode2);
        var validationInfoByPattern = GetValidationInfoFromTaxNumberInfo(result.FormattedTaxNumber, taxNumberInfo);

        if (validationInfoByPattern == null)
        {
            result.AddErrorIfNotEmpty(ValidationErrors.CantMatchValidationPatternError);
            return result;
        }

        var formattedTaxNumber = result.FormattedTaxNumber;

        result.AddErrorsIfNotEmpty(TaxNumberValidationHelper.ValidateRegex(
            formattedTaxNumber,
            validationInfoByPattern.Regex,
            taxNumber,
            validationInfoByPattern.ValidationFormatDescription));

        ValidateLength(result, validationInfoByPattern);

        var digitPart = new string(result.FormattedTaxNumber.Where(char.IsDigit).ToArray());
        if (!ValidateDigitPart(digitPart, result))
        {
            return result;
        }

        ValidateChecksum(result, validationInfoByPattern, digitPart);

        if (shouldValidateViaApi)
        {
            ValidateWithOnlineService(taxNumberInfo, formattedTaxNumber, result);
        }

        return result;
    }

    private static void ValidateWithOnlineService(
        TaxNumberDefinition taxNumberInfo,
        string formattedTaxNumber,
        TaxNumberValidationResult result)
    {
        // TODO: handle API not available case
        HttpResponseMessage? apiResult;
        switch (taxNumberInfo.VerificationApi)
        {
            case VerificationApi.EuropeVies:
                {
                    using var httpClient = new HttpClient();
                    apiResult = httpClient.Send(new HttpRequestMessage
                    {
                        RequestUri = new Uri($"https://ec.europa.eu/taxation_customs/vies/rest-api/ms/{taxNumberInfo.Country}/tax/{formattedTaxNumber.Substring(2)}"),
                        Method = HttpMethod.Get
                    });
                    var viesResult = JsonSerializer.Deserialize<ViesVerificationResponse>(apiResult.Content.ReadAsStringAsync().Result)!;
                    result.ApiVerificationData = viesResult;

                    if (!viesResult.isValid)
                    {
                        result.AddErrorIfNotEmpty(ValidationErrors.ApiValidationError);
                    }

                    break;
                }

            case VerificationApi.None:
            case VerificationApi.GSTIN:
            default:
                break;
        }
    }

    private static void ValidateChecksum(
        TaxNumberValidationResult result,
        TaxNumberValidationRule validationInfoByPattern,
        string digitPart)
    {
        if (validationInfoByPattern.Checksum == null)
        {
            return;
        }

        if (digitPart.Length < validationInfoByPattern.Checksum.GetWeights().Length)
        {
            result.AddErrorIfNotEmpty(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.Checksum.GetWeights().Length));
            return;
        }

        var checksumDigitPosition = CalculateChecksumDigitPosition(validationInfoByPattern, result, digitPart);
        if (!checksumDigitPosition.HasValue)
        {
            return;
        }

        ValidateChecksumByCountry(result, validationInfoByPattern, digitPart, checksumDigitPosition.Value);
    }

    private static void ValidateChecksumByCountry(
        TaxNumberValidationResult result,
        TaxNumberValidationRule validationInfoByPattern,
        string digitPart,
        int checksumDigitPosition)
    {
        int minimumLength;
        var formattedVatNumber = result.FormattedTaxNumber;
        var firstTypeLength = 0;
        var secondTypeLength = 0;

        switch (validationInfoByPattern.Checksum!.Algorithm)
        {

            case ChecksumAlgorithm.Luhn:
                minimumLength = 6;
                if (digitPart.Length < minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumValidationHelper.ValidateLuhnDigitNumber(digitPart));
                break;

            case ChecksumAlgorithm.ModAndSubstract:
                if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                    !(validationInfoByPattern.Checksum.GetWeights().Any()))
                {
                    result.AddErrorIfNotEmpty(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrorsIfNotEmpty(ChecksumValidationHelper.ValidateModAndSubstract(digitPart, validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.GetWeights(), checksumDigitPosition));
                break;

            case ChecksumAlgorithm.Mod:
                if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                    (validationInfoByPattern.Checksum.GetWeights().Length <= 0))
                {
                    result.AddErrorIfNotEmpty(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrorsIfNotEmpty(ChecksumValidationHelper.ValidateMod(digitPart, validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.GetWeights(), checksumDigitPosition));
                break;

            case ChecksumAlgorithm.CO_Algorithm:
                minimumLength = 1;
                if (digitPart.Length < minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                // Same as Colombian VAT
                result.AddErrorsIfNotEmpty(ChecksumVatValidationHelper.ValidateVatCOAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.CH_Tax_Algorithm:
                minimumLength = 13;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxCHAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.BR_Tax_Algorithm:
                minimumLength = 11;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxBRAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.IT_Tax_Algorithm:
                minimumLength = 16;
                if (formattedVatNumber.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxITAlgorithm(formattedVatNumber));
                break;

            case ChecksumAlgorithm.DE_Tax_Algorithm:
                minimumLength = 11;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxDEAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.NL_Tax_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxNLAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.MX_Tax1_Algorithm:
                minimumLength = 12;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxMX1Algorithm(digitPart));
                break;

            case ChecksumAlgorithm.MX_Tax2_Algorithm:
                minimumLength = 13;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxMX2Algorithm(digitPart));
                break;

            case ChecksumAlgorithm.IN_Tax_Algorithm:
                minimumLength = 10;
                if (formattedVatNumber.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxINAlgorithm(formattedVatNumber));
                break;

            case ChecksumAlgorithm.CA_Tax_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxCAAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.BE_Tax_Algorithm:
                minimumLength = 11;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxBEAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.AU_Tax_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxAUAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.PL_Tax_Algorithm:
                minimumLength = 11;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxPLAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.DK_Tax_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxATAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.TR_Tax_Algorithm:
                minimumLength = 11;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxTRAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.SE_Tax_Algorithm:
                minimumLength = 10;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxSEAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.IL_Tax_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxILAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.RU_Algorithm:
                firstTypeLength = 10;
                secondTypeLength = 12;
                if (digitPart.Length != firstTypeLength &&
                    digitPart.Length != secondTypeLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, $"{firstTypeLength}' or '{secondTypeLength}"));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumVatValidationHelper.ValidateVatRUAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.NO_Tax_Algorithm:
                minimumLength = 11;
                if (digitPart.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxNOAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.NZ_Tax_Algorithm:
                firstTypeLength = 8;
                secondTypeLength = 9;
                if (digitPart.Length != firstTypeLength &&
                    digitPart.Length != secondTypeLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, $"{firstTypeLength}' or '{secondTypeLength}"));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxNZAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.FI_Tax_Algorithm:
                minimumLength = 11;
                if (formattedVatNumber.Length != minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumTaxValidationHelper.ValidateTaxFIAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.ID_Algorithm:
                minimumLength = 12;
                if (digitPart.Length < minimumLength)
                {
                    result.AddErrorIfNotEmpty(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrorsIfNotEmpty(ChecksumVatValidationHelper.ValidateVatIDAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.None:
                break;

            default:
                result.AddErrorIfNotEmpty(ValidationErrors.UnknownChecksumAlgorithm);
                break;
        }
    }

        private static int? CalculateChecksumDigitPosition(TaxNumberValidationRule validationInfoByPattern, TaxNumberValidationResult result, string digitPart)
    {
        var checksumDigitPosition = -1;
        var checksumDigitValue = validationInfoByPattern.Checksum!.ChecksumDigit ?? "Last";

        if (checksumDigitValue != "-1" && checksumDigitValue != "Last")
        {
            if (!int.TryParse(checksumDigitValue, out var number))
            {
                result.AddErrorIfNotEmpty(ValidationErrors.ChecksumDigitPositionNotNumeric);
                return null;
            }

            checksumDigitPosition = number;
        }

        if (checksumDigitPosition > 0)
        {
            // Remove country offset as we are operating with digit part
            checksumDigitPosition -= 2;

            // add +1 since length is not from 0 and check digit position is from 0
            if (digitPart.Length < checksumDigitPosition + 1)
            {
                result.AddErrorIfNotEmpty(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.Checksum.GetWeights().Count()));
                return null;
            }
        }

        return checksumDigitPosition;
    }

    private static bool ValidateDigitPart(string digitPart, TaxNumberValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(digitPart))
        {
            result.AddErrorIfNotEmpty(ValidationErrors.EmptyTaxNumberError);
            return false;
        }

        if (!decimal.TryParse(digitPart, out var parsedNumber) ||
            parsedNumber <= 0)
        {
            result.AddErrorIfNotEmpty(ValidationErrors.ChecksumShoulBeBiggerThanZero);
            return false;
        }

        return true;
    }

    private static void ValidateLength(
        TaxNumberValidationResult result,
        TaxNumberValidationRule validationInfoByPattern)
    {
        if (result.FormattedTaxNumber.Length > validationInfoByPattern.MaximumLength)
        {
            result.AddErrorIfNotEmpty(string.Format(ValidationErrors.MaximumNumbericLengthError, validationInfoByPattern.MaximumLength));
        }

        if (result.FormattedTaxNumber.Length < validationInfoByPattern.MinimumLength)
        {
            result.AddErrorIfNotEmpty(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.MinimumLength));
        }
    }

    private static TaxNumberValidationRule? GetValidationInfoFromTaxNumberInfo(string formattedTaxNumber, TaxNumberDefinition taxNumberDefinition)
    {
        TaxNumberValidationRule? validationInfoByPattern = null;

        if (taxNumberDefinition.ValidationRules.Count == 1)
        {
            validationInfoByPattern = taxNumberDefinition.ValidationRules[0];
        }
        else
        {
            foreach (var validation in taxNumberDefinition.ValidationRules!)
            {
                if (Regex.IsMatch(formattedTaxNumber, validation.Regex))
                {
                    validationInfoByPattern = validation;
                }
            }
        }

        return validationInfoByPattern;
    }
}