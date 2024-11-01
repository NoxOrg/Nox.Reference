﻿using Nox.Reference.Data.World.Helpers;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Nox.Reference;

// Important: not production ready, should be tested and refined
public static class GenericVatValidationService
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
        var result = VatNumberValidationResult.CreateWithValidaton(vatNumber, vatNumberInfo.Country.AlphaCode2);
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
                        RequestUri = new Uri($"https://ec.europa.eu/taxation_customs/vies/rest-api/ms/{vatNumberInfo.Country.AlphaCode2}/vat/{formattedVatNumber.Substring(2)}"),
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

                result.AddErrors(ChecksumValidationHelper.ValidateLuhnDigitNumber(digitPart));
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

                result.AddErrors(ChecksumValidationHelper.ValidateModAndSubstract(digitPart, validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.GetWeights(), checksumDigitPosition));
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

                result.AddErrors(ChecksumVatValidationHelper.ValidateModAndSubstractItaly(digitPart, validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.GetWeights()));
                break;

            case ChecksumAlgorithm.Mod:
                if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                    (validationInfoByPattern.Checksum.GetWeights().Length <= 0))
                {
                    result.AddError(
                        string.Format(
                            ValidationErrors.NotEnoughParametersProvidedToChecksum,
                            $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                    break;
                }

                result.AddErrors(ChecksumValidationHelper.ValidateMod(digitPart, validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.GetWeights(), checksumDigitPosition));
                break;

            case ChecksumAlgorithm.MX_Algorithm:
                // length is checked inside

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatMXAlgorithm(result.OriginalVatNumber));
                break;

            case ChecksumAlgorithm.DE_Algorithm:
                minimumLength = 9;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatDEAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.FR_Algorithm:
                minimumLength = 2;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatFRAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.CO_Algorithm:
                minimumLength = 1;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatCOAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.AU_Algorithm:
                minimumLength = 9;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatAUAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.BE_Algorithm:
                minimumLength = 9;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatBEAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.BR_Algorithm:
                minimumLength = 14;
                if (digitPart.Length != minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatBRAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.CA_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatCAAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.CH_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatCHAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.GB_Algorithm:
                minimumLength = 9;
                if (digitPart.Length != minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatGBAlgorithm(digitPart));
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

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatES1Algorithm(formattedVatNumber.Substring(3)));
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

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatES2Algorithm(formattedVatNumber.Substring(3)));
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

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatES3Algorithm(formattedVatNumber.Substring(2)));
                break;

            case ChecksumAlgorithm.DK_Algorithm:
                minimumLength = 8;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatDKAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.AT_Algorithm:
                minimumLength = 8;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatATAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.JP_Algorithm:
                minimumLength = 12;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatJPAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.CN_Algorithm:
                minimumLength = 18;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatCNAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.TR_Algorithm:
                minimumLength = 10;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatTRAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.SE_Algorithm:
                minimumLength = 10;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatSEAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.NO_Algorithm:
                minimumLength = 9;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatNOAlgorithm(digitPart));
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

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatRUAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.NZ_Algorithm:
                minimumLength = 8;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatNZAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.ID_Algorithm:
                minimumLength = 12;
                if (digitPart.Length < minimumLength)
                {
                    result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                    return;
                }

                result.AddErrors(ChecksumVatValidationHelper.ValidateVatIDAlgorithm(digitPart));
                break;

            case ChecksumAlgorithm.None:
                break;

            default:
                result.AddError(ValidationErrors.UnknownChecksumAlgorithm);
                break;
        }
    }
}