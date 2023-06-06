using System.Text.Json;
using System.Text.RegularExpressions;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World;

public static class GenericValidationService
{
    /// <summary>
    /// Validates vat number string and returns validation result
    /// </summary>
    /// <param name="vatNumber">Vat number as text</param>
    /// <param name="vatNumberInfo">Information that will be used for validation process</param>
    /// <param name="shouldValidateViaApi">Flag to determine if validation should use online API service (if applicable) or not</param>
    /// <returns>Validation result</returns>
    public static VatNumberValidationResult ValidateVatNumber(
        string vatNumber,
        VatNumberDefinition vatNumberInfo,
        bool shouldValidateViaApi = true)
    {
        var result = VatNumberValidationResult.CreateWithValidaton(vatNumber, vatNumberInfo.Country);
        var validationInfoByPattern = GetValidationInfoFromVatNumberInfo(result.FormattedVatNumber, vatNumberInfo);

        if (validationInfoByPattern == null)
        {
            result.AddError(ValidationErrors.CantMatchValidationPatternError);
            return result;
        }

        var formattedVatNumber = result.FormattedVatNumber;

        result.AddErrors(VatNumberValidationHelper.ValidateRegex(
            formattedVatNumber,
            validationInfoByPattern.Regex,
            vatNumber,
            validationInfoByPattern.ValidationFormatDescription));

        ValidateLength(result, validationInfoByPattern);

        var digitPart = new string(result.FormattedVatNumber.Where(char.IsDigit).ToArray());
        if (!ValidateDigitPart(digitPart, result))
        {
            return result;
        }

        ValidateChecksum(result, validationInfoByPattern, digitPart);

        if (shouldValidateViaApi)
        {
            ValidateWithOnlineService(vatNumberInfo, formattedVatNumber, result);
        }

        return result;
    }

    private static void ValidateWithOnlineService(
        VatNumberDefinition vatNumberInfo,
        string formattedVatNumber,
        VatNumberValidationResult result)
    {
        // TODO: handle API not available case
        HttpResponseMessage? apiResult;
        switch (vatNumberInfo.VerificationApi)
        {
            case VerificationApi.EuropeVies:
                {
                    using var httpClient = new HttpClient();
                    apiResult = httpClient.Send(new HttpRequestMessage
                    {
                        RequestUri = new Uri($"https://ec.europa.eu/taxation_customs/vies/rest-api/ms/{vatNumberInfo.Country}/vat/{formattedVatNumber.Substring(2)}"),
                        Method = HttpMethod.Get
                    });
                    var viesResult = JsonSerializer.Deserialize<ViesVerificationResponse>(apiResult.Content.ReadAsStringAsync().Result)!;
                    result.ApiVerificationData = viesResult;

                    if (!viesResult.isValid)
                    {
                        result.AddError(ValidationErrors.ApiValidationError);
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
        VatNumberValidationResult result,
        VatNumberValidationRule validationInfoByPattern,
        string digitPart)
    {
        if (validationInfoByPattern.Checksum == null)
        {
            return;
        }

        if (digitPart.Length < validationInfoByPattern.Checksum.GetWeights().Length)
        {
            result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.Checksum.GetWeights().Length));
            return;
        }

        var checksumDigitPosition = CalculateChecksumDigitPosition(validationInfoByPattern, result, digitPart);
        if (!checksumDigitPosition.HasValue)
        {
            return;
        }

        ValidateChecksumByCountry(result, validationInfoByPattern, digitPart, checksumDigitPosition.Value);
    }

    private static int? CalculateChecksumDigitPosition(VatNumberValidationRule validationInfoByPattern, VatNumberValidationResult result, string digitPart)
    {
        var checksumDigitPosition = -1;
        var checksumDigitValue = validationInfoByPattern.Checksum!.ChecksumDigit ?? "Last";

        if (checksumDigitValue != "-1" && checksumDigitValue != "Last")
        {
            if (!int.TryParse(checksumDigitValue, out var number))
            {
                result.AddError(ValidationErrors.ChecksumDigitPositionNotNumeric);
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
                result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.Checksum.GetWeights().Count()));
                return null;
            }
        }

        return checksumDigitPosition;
    }

    private static bool ValidateDigitPart(string digitPart, VatNumberValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(digitPart))
        {
            result.AddError(ValidationErrors.EmptyVatNumberError);
            return false;
        }

        if (!decimal.TryParse(digitPart, out var parsedNumber) ||
            parsedNumber <= 0)
        {
            result.AddError(ValidationErrors.ChecksumShoulBeBiggerThanZero);
            return false;
        }

        return true;
    }

    private static void ValidateLength(
        VatNumberValidationResult result,
        VatNumberValidationRule validationInfoByPattern)
    {
        if (result.FormattedVatNumber.Length > validationInfoByPattern.MaximumLength)
        {
            result.AddError(string.Format(ValidationErrors.MaximumNumbericLengthError, validationInfoByPattern.MaximumLength));
        }

        if (result.FormattedVatNumber.Length < validationInfoByPattern.MinimumLength)
        {
            result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.MinimumLength));
        }
    }

    private static VatNumberValidationRule? GetValidationInfoFromVatNumberInfo(string formattedVatNumber, VatNumberDefinition vatNumberDefinition)
    {
        VatNumberValidationRule? validationInfoByPattern = null;

        if (vatNumberDefinition.ValidationRules.Count == 1)
        {
            validationInfoByPattern = vatNumberDefinition.ValidationRules[0];
        }
        else
        {
            foreach (var validation in vatNumberDefinition.ValidationRules!)
            {
                if (Regex.IsMatch(formattedVatNumber, validation.Regex))
                {
                    validationInfoByPattern = validation;
                }
            }
        }

        return validationInfoByPattern;
    }

    private static void ValidateChecksumByCountry(
        VatNumberValidationResult result,
        VatNumberValidationRule validationInfoByPattern,
        string digitPart,
        int checksumDigitPosition)
    {
        int minimumLength;
        var formattedVatNumber = result.FormattedVatNumber;

        switch (validationInfoByPattern.Checksum!.Algorithm)
        {
            case ChecksumAlgorithm.Luhn:
                minimumLength = 6;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateLuhnDigitForVatNumber());
                break;

            case ChecksumAlgorithm.ModAndSubstract:
                if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                    !(validationInfoByPattern.Checksum.GetWeights().Any()))
                {
                    result.AddError(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrors(digitPart.ValidateModAndSubstract(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.GetWeights(), checksumDigitPosition));
                break;

            case ChecksumAlgorithm.ModAndSubstract_IT:
                if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                    !validationInfoByPattern.Checksum.GetWeights().Any())
                {
                    result.AddError(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrors(digitPart.ValidateModAndSubstractItaly(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.GetWeights()));
                break;

            case ChecksumAlgorithm.Mod:
                if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                    !(validationInfoByPattern.Checksum.GetWeights().Length > 0))
                {
                    result.AddError(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrors(digitPart.ValidateMod(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.GetWeights(), checksumDigitPosition));
                break;

            case ChecksumAlgorithm.MX_Algorithm:
                // length is checked inside

                result.AddErrors(result.OriginalVatNumber.ValidateMXAlgorithm());
                break;

            case ChecksumAlgorithm.DE_Algorithm:
                minimumLength = 9;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateDEAlgorithm());
                break;

            case ChecksumAlgorithm.FR_Algorithm:
                minimumLength = 2;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateFRAlgorithm());
                break;

            case ChecksumAlgorithm.CO_Algorithm:
                minimumLength = 1;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateCOAlgorithm());
                break;

            case ChecksumAlgorithm.AU_Algorithm:
                minimumLength = 9;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateAUAlgorithm());
                break;

            case ChecksumAlgorithm.BE_Algorithm:
                minimumLength = 9;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateBEAlgorithm());
                break;

            case ChecksumAlgorithm.BR_Algorithm:
                minimumLength = 14;
                if (digitPart.Length != minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateBRAlgorithm());
                break;

            case ChecksumAlgorithm.CA_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateCAAlgorithm());
                break;

            case ChecksumAlgorithm.CH_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateCHAlgorithm());
                break;

            case ChecksumAlgorithm.GB_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateGBAlgorithm());
                break;

            case ChecksumAlgorithm.ES_Algorithm1:
                minimumLength = 10;
                if (formattedVatNumber.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                if (result.ValidationErrors.Count > 0)
                {
                    result.AddError(ValidationErrors.ChecksumError);
                    return;
                }

                result.AddErrors(formattedVatNumber.Substring(3).ValidateES1Algorithm());
                break;

            case ChecksumAlgorithm.ES_Algorithm2:
                minimumLength = 10;
                if (formattedVatNumber.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                if (result.ValidationErrors.Count > 0)
                {
                    result.AddError(ValidationErrors.ChecksumError);
                    return;
                }

                result.AddErrors(formattedVatNumber.Substring(3).ValidateES2Algorithm());
                break;

            case ChecksumAlgorithm.ES_Algorithm3:
                minimumLength = 9;
                if (formattedVatNumber.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                if (result.ValidationErrors.Count > 0)
                {
                    result.AddError(ValidationErrors.ChecksumError);
                    return;
                }

                result.AddErrors(formattedVatNumber.Substring(2).ValidateES3Algorithm());
                break;

            case ChecksumAlgorithm.DK_Algorithm:
                minimumLength = 8;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateDKAlgorithm());
                break;

            case ChecksumAlgorithm.AT_Algorithm:
                minimumLength = 8;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateATAlgorithm());
                break;

            case ChecksumAlgorithm.JP_Algorithm:
                minimumLength = 12;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateJPAlgorithm());
                break;

            case ChecksumAlgorithm.CN_Algorithm:
                minimumLength = 18;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateCNAlgorithm());
                break;

            case ChecksumAlgorithm.TR_Algorithm:
                minimumLength = 10;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateTRAlgorithm());
                break;

            case ChecksumAlgorithm.SE_Algorithm:
                minimumLength = 10;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateSEAlgorithm());
                break;

            case ChecksumAlgorithm.NO_Algorithm:
                minimumLength = 9;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateNOAlgorithm());
                break;

            case ChecksumAlgorithm.RU_Algorithm:
                var firstTypeLength = 10;
                var secondTypeLength = 12;
                if (digitPart.Length != firstTypeLength &&
                    digitPart.Length != secondTypeLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, firstTypeLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateRUAlgorithm());
                break;

            case ChecksumAlgorithm.NZ_Algorithm:
                minimumLength = 8;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateNZAlgorithm());
                break;

            case ChecksumAlgorithm.ID_Algorithm:
                minimumLength = 12;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(digitPart.ValidateIDAlgorithm());
                break;

            case ChecksumAlgorithm.None:
                break;

            default:
                result.AddError(ValidationErrors.UnknownChecksumAlgorithm);
                break;
        }
    }
}