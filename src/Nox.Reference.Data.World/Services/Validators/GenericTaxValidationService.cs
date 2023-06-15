using System.Text.Json;
using System.Text.RegularExpressions;

namespace Nox.Reference.Data.World;

// TODO: join with vat number service into single service if possible
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
            result.AddError(ValidationErrors.CantMatchValidationPatternError);
            return result;
        }

        var formattedTaxNumber = result.FormattedTaxNumber;

        result.AddErrors(TaxNumberValidationHelper.ValidateRegex(
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

    private static void ValidateChecksumByCountry(
        TaxNumberValidationResult result,
        TaxNumberValidationRule validationInfoByPattern,
        string digitPart,
        int checksumDigitPosition)
    {
        int minimumLength;
        //var formattedTaxNumber = result.FormattedTaxNumber;

        switch (validationInfoByPattern.Checksum!.Algorithm)
        {
            //case ChecksumAlgorithm.Luhn:
            //    minimumLength = 6;
            //    if (digitPart.Length < minimumLength)
            //    {
            //        result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
            //        return;
            //    }

            //    result.AddErrors(digitPart.ValidateLuhnDigitForVatNumber());
            //    break;

            case ChecksumAlgorithm.None:
                break;

            default:
                result.AddError(ValidationErrors.UnknownChecksumAlgorithm);
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

    private static bool ValidateDigitPart(string digitPart, TaxNumberValidationResult result)
    {
        if (string.IsNullOrWhiteSpace(digitPart))
        {
            result.AddError(ValidationErrors.EmptyTaxNumberError);
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
        TaxNumberValidationResult result,
        TaxNumberValidationRule validationInfoByPattern)
    {
        if (result.FormattedTaxNumber.Length > validationInfoByPattern.MaximumLength)
        {
            result.AddError(string.Format(ValidationErrors.MaximumNumbericLengthError, validationInfoByPattern.MaximumLength));
        }

        if (result.FormattedTaxNumber.Length < validationInfoByPattern.MinimumLength)
        {
            result.AddError(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.MinimumLength));
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