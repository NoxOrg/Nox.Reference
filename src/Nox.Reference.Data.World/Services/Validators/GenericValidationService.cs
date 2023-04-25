using Nox.Reference.Abstractions;
using Nox.Reference.Common;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Nox.Reference.Data.World;

public class GenericValidationService : VatValidationServiceBase
{
    public static HttpClient _httpClient { get; set; } = new HttpClient();

    protected override IVatNumberValidationResult DoValidateVatNumber(
        string vatNumber,
        IVatNumberDefinitionInfo vatNumberInfo,
        bool shouldValidateViaApi = true)
    {
        var formattedVatNumber = vatNumber.NormalizeVatNumber(vatNumberInfo.Country);
        var result = VatNumberValidationResult.CreateWithValidaton(formattedVatNumber);

        IValidationInfo? validationInfoByPattern = GetValidationInfoFromVatNumberInfo(vatNumber, vatNumberInfo);
        if (validationInfoByPattern == null)
        {
            result.AddError(ValidationErrors.CantMatchValidationPatternError);
            return result;
        }

        result.AddErrors(VatNumberValidationHelper.ValidateRegex(
            formattedVatNumber,
            validationInfoByPattern.Regex,
            vatNumber,
            validationInfoByPattern.ValidationFormatDescription));

        ValidateLength(formattedVatNumber, result, validationInfoByPattern);

        var digitPart = new string(formattedVatNumber.Where(char.IsDigit).ToArray());
        if (!ValidateDigitPart(digitPart, result))
        {
            return result;
        }

        ValidateChecksum(formattedVatNumber, result, validationInfoByPattern, digitPart);

        if (shouldValidateViaApi)
        {
            ValidateWithOnlineService(vatNumberInfo, formattedVatNumber, result);
        }

        return result;
    }

    private static void ValidateWithOnlineService(
        IVatNumberDefinitionInfo vatNumberInfo,
        string formattedVatNumber,
        VatNumberValidationResult result)
    {
        HttpResponseMessage? apiResult;
        switch (vatNumberInfo.VerificationApi)
        {
            case VerificationApi.EuropeVies:
                apiResult = _httpClient.Send(new HttpRequestMessage
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

            case VerificationApi.None:
            case VerificationApi.GSTIN:
            default:
                break;
        }
    }

    private void ValidateChecksum(
        string formattedVatNumber,
        VatNumberValidationResult result,
        IValidationInfo validationInfoByPattern,
        string digitPart)
    {
        if (validationInfoByPattern.Checksum == null)
        {
            return;
        }

        if (digitPart.Length < validationInfoByPattern.Checksum.Weights?.Count)
        {
            result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.Checksum.Weights?.Count));
            return;
        }

        var checksumDigitPosition = CalculateChecksumDigitPosition(validationInfoByPattern, result, digitPart);
        if (!checksumDigitPosition.HasValue)
        {
            return;
        }

        ValidateChecksumByCountry(formattedVatNumber, result, validationInfoByPattern, digitPart, checksumDigitPosition.Value);
    }

    private static int? CalculateChecksumDigitPosition(IValidationInfo validationInfoByPattern, VatNumberValidationResult result, string digitPart)
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
                result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.Checksum.Weights?.Count));
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
        string formattedVatNumber,
        VatNumberValidationResult result,
        IValidationInfo validationInfoByPattern)
    {
        if (formattedVatNumber.Length > validationInfoByPattern.MaximumLength)
        {
            result.AddError(string.Format(ValidationErrors.MaximumNumbericLengthError, validationInfoByPattern.MaximumLength));
        }

        if (formattedVatNumber.Length < validationInfoByPattern.MinimumLength)
        {
            result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.MinimumLength));
        }
    }

    private static IValidationInfo? GetValidationInfoFromVatNumberInfo(string vatNumber, IVatNumberDefinitionInfo vatNumberInfo)
    {
        IValidationInfo? validationInfoByPattern = null;

        if (vatNumberInfo.Validations!.Length == 1)
        {
            validationInfoByPattern = vatNumberInfo.Validations[0];
        }
        else
        {
            foreach (var validation in vatNumberInfo.Validations!)
            {
                if (Regex.IsMatch(vatNumber.NormalizeVatNumber(vatNumberInfo.Country), validation.Regex))
                {
                    validationInfoByPattern = validation;
                }
            }
        }

        return validationInfoByPattern;
    }

    private static void ValidateChecksumByCountry(
        string formattedVatNumber,
        VatNumberValidationResult result,
        IValidationInfo validationInfoByPattern,
        string digitPart,
        int checksumDigitPosition)
    {
        int minimumLength;

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
                    !(validationInfoByPattern.Checksum.Weights?.Count > 0))
                {
                    result.AddError(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrors(digitPart.ValidateModAndSubstract(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.Weights, checksumDigitPosition));
                break;

            case ChecksumAlgorithm.ModAndSubstract_IT:
                if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                    !(validationInfoByPattern.Checksum.Weights?.Count > 0))
                {
                    result.AddError(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrors(digitPart.ValidateModAndSubstractItaly(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.Weights));
                break;

            case ChecksumAlgorithm.Mod:
                if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                    !(validationInfoByPattern.Checksum.Weights?.Count > 0))
                {
                    result.AddError(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrors(digitPart.ValidateMod(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.Weights, checksumDigitPosition));
                break;

            case ChecksumAlgorithm.MX_Algorithm:
                // length is checked inside

                result.AddErrors(formattedVatNumber.ValidateMXAlgorithm());
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