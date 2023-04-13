using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Nox.Reference.VatNumbers.Services.Validators
{
    public class GenericValidationService : VatValidationServiceBase
    {
        public static HttpClient _httpClient { get; set; } = new HttpClient();

        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber, bool shouldValidateViaApi = true)
        {
            var result = new ValidationResult();

            IValidationInfo? validationInfoByPattern = GetValidationInfoFromVatNumberInfo(vatNumber);
            if (validationInfoByPattern == null)
            {
                result.ValidationErrors.Add(ValidationErrors.CantMatchValidationPatternError);
                return result;
            }

            result.ValidationErrors.AddRange(ValidateRegex(vatNumber.FormattedVatNumber, validationInfoByPattern.Regex, vatNumber.OriginalVatNumber, validationInfoByPattern.ValidationFormatDescription));

            ValidateLength(vatNumber, result, validationInfoByPattern);

            var digitPart = new string(vatNumber.FormattedVatNumber.Where(char.IsDigit).ToArray());
            if (!ValidateDigitPart(digitPart, result))
            {
                return result;
            }

            ValidateChecksum(vatNumber, result, validationInfoByPattern, digitPart);

            if (shouldValidateViaApi)
            {
                ValidateWithOnlineService(vatNumber, result);
            }

            return result;
        }

        private void ValidateWithOnlineService(IVatNumberInfo vatNumber, ValidationResult result)
        {
            HttpResponseMessage? apiResult;
            switch (vatNumber.VerificationApi)
            {
                case VerificationApi.EuropeVies:
                    apiResult = _httpClient.Send(new HttpRequestMessage
                    {
                        RequestUri = new Uri($"https://ec.europa.eu/taxation_customs/vies/rest-api/ms/{vatNumber.Country}/vat/{vatNumber.FormattedVatNumber.Substring(2)}"),
                        Method = HttpMethod.Get
                    });
                    var viesResult = JsonSerializer.Deserialize<ViesVerificationResponse>(apiResult.Content.ReadAsStringAsync().Result)!;
                    vatNumber.ApiVerificationData = viesResult;

                    if (!viesResult.isValid)
                    {
                        result.ValidationErrors.Add(ValidationErrors.ApiValidationError);
                    }

                    break;
                case VerificationApi.None:
                case VerificationApi.GSTIN:
                default:
                    break;
            }
        }

        private void ValidateChecksum(IVatNumberInfo vatNumber, ValidationResult result, IValidationInfo validationInfoByPattern, string digitPart)
        {
            if (validationInfoByPattern.Checksum == null)
            {
                return;
            }

            if (digitPart.Length < validationInfoByPattern.Checksum.Weights?.Count)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.Checksum.Weights?.Count));
                return;
            }

            var checksumDigitPosition = CalculateChecksumDigitPosition(validationInfoByPattern, result, digitPart);
            if (!checksumDigitPosition.HasValue)
            {
                return;
            }

            ValidateChecksumByCountry(vatNumber, result, validationInfoByPattern, digitPart, checksumDigitPosition.Value);
        }

        private int? CalculateChecksumDigitPosition(IValidationInfo validationInfoByPattern, ValidationResult result, string digitPart)
        {
            var checksumDigitPosition = -1;
            var checksumDigitValue = validationInfoByPattern.Checksum!.ChecksumDigit ?? "Last";

            if (checksumDigitValue != "-1" && checksumDigitValue != "Last")
            {
                if (!int.TryParse(checksumDigitValue, out var number))
                {
                    result.ValidationErrors.Add(ValidationErrors.ChecksumDigitPositionNotNumeric);
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
                    result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.Checksum.Weights?.Count));
                    return null;
                }
            }

            return checksumDigitPosition;
        }

        private bool ValidateDigitPart(string digitPart, ValidationResult result)
        {
            if (string.IsNullOrWhiteSpace(digitPart))
            {
                result.ValidationErrors.Add(ValidationErrors.EmptyVatNumberError);
                return false;
            }

            if (!decimal.TryParse(digitPart, out var parsedNumber) ||
                parsedNumber <= 0)
            {
                result.ValidationErrors.Add(ValidationErrors.ChecksumShoulBeBiggerThanZero);
                return false;
            }

            return true;
        }

        private static void ValidateLength(IVatNumberInfo vatNumber, ValidationResult result, IValidationInfo validationInfoByPattern)
        {
            if (vatNumber.FormattedVatNumber.Length > validationInfoByPattern.MaximumLength)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MaximumNumbericLengthError, validationInfoByPattern.MaximumLength));
            }

            if (vatNumber.FormattedVatNumber.Length < validationInfoByPattern.MinimumLength)
            {
                result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, validationInfoByPattern.MinimumLength));
            }
        }

        private static IValidationInfo? GetValidationInfoFromVatNumberInfo(IVatNumberInfo vatNumber)
        {
            IValidationInfo? validationInfoByPattern = null;

            if (vatNumber.Validations!.Length == 1)
            {
                validationInfoByPattern = vatNumber.Validations[0];
            }
            else
            {
                foreach (var validation in vatNumber.Validations!)
                {
                    if (Regex.IsMatch(vatNumber.FormattedVatNumber, validation.Regex))
                    {
                        validationInfoByPattern = validation;
                    }
                }
            }

            return validationInfoByPattern;
        }

        private void ValidateChecksumByCountry(IVatNumberInfo vatNumber, ValidationResult result, IValidationInfo validationInfoByPattern, string digitPart, int checksumDigitPosition)
        {
            int minimumLength;

            switch (validationInfoByPattern.Checksum!.Algorithm)
            {
                case ChecksumAlgorithm.Luhn:
                    minimumLength = 6;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateLuhnDigitForVatNumber());
                    break;
                case ChecksumAlgorithm.ModAndSubstract:
                    if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                        !(validationInfoByPattern.Checksum.Weights?.Count > 0))
                    {
                        result.ValidationErrors.Add(
                            string.Format(
                                ValidationErrors.NotEnoughParametersProvidedToChecksum,
                                $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                        break;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateModAndSubstract(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.Weights, checksumDigitPosition));
                    break;
                case ChecksumAlgorithm.ModAndSubstract_IT:
                    if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                        !(validationInfoByPattern.Checksum.Weights?.Count > 0))
                    {
                        result.ValidationErrors.Add(
                            string.Format(
                                ValidationErrors.NotEnoughParametersProvidedToChecksum,
                                $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                        break;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateModAndSubstractItaly(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.Weights));
                    break;
                case ChecksumAlgorithm.Mod:
                    if (!validationInfoByPattern.Checksum.Modulus.HasValue ||
                        !(validationInfoByPattern.Checksum.Weights?.Count > 0))
                    {
                        result.ValidationErrors.Add(
                            string.Format(
                                ValidationErrors.NotEnoughParametersProvidedToChecksum,
                                $"{nameof(validationInfoByPattern.Checksum.Modulus)},{nameof(validationInfoByPattern.Checksum.Weights)}"));
                        break;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateMod(validationInfoByPattern.Checksum.Modulus.Value, validationInfoByPattern.Checksum.Weights, checksumDigitPosition));
                    break;
                case ChecksumAlgorithm.MX_Algorithm:
                    // length is checked inside 

                    result.ValidationErrors.AddRange(vatNumber.OriginalVatNumber.ValidateMXAlgorithm());
                    break;
                case ChecksumAlgorithm.DE_Algorithm:
                    minimumLength = 9;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateDEAlgorithm());
                    break;
                case ChecksumAlgorithm.FR_Algorithm:
                    minimumLength = 2;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateFRAlgorithm());
                    break;
                case ChecksumAlgorithm.CO_Algorithm:
                    minimumLength = 1;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateCOAlgorithm());
                    break;
                case ChecksumAlgorithm.AU_Algorithm:
                    minimumLength = 9;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateAUAlgorithm());
                    break;
                case ChecksumAlgorithm.BE_Algorithm:
                    minimumLength = 9;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateBEAlgorithm());
                    break;
                case ChecksumAlgorithm.BR_Algorithm:
                    minimumLength = 14;
                    if (digitPart.Length != minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateBRAlgorithm());
                    break;

                case ChecksumAlgorithm.CA_Algorithm:
                    minimumLength = 9;
                    if (digitPart.Length != minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateCAAlgorithm());
                    break;

                case ChecksumAlgorithm.CH_Algorithm:
                    minimumLength = 9;
                    if (digitPart.Length != minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateCHAlgorithm());
                    break;

                case ChecksumAlgorithm.GB_Algorithm:
                    minimumLength = 9;
                    if (digitPart.Length != minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateGBAlgorithm());
                    break;

                case ChecksumAlgorithm.ES_Algorithm1:
                    minimumLength = 10;
                    if (vatNumber.FormattedVatNumber.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    if (result.ValidationErrors.Count > 0)
                    {
                        result.ValidationErrors.Add(ValidationErrors.ChecksumError);
                        return;
                    }

                    result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(3).ValidateES1Algorithm());
                    break;

                case ChecksumAlgorithm.ES_Algorithm2:
                    minimumLength = 10;
                    if (vatNumber.FormattedVatNumber.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    if (result.ValidationErrors.Count > 0)
                    {
                        result.ValidationErrors.Add(ValidationErrors.ChecksumError);
                        return;
                    }

                    result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(3).ValidateES2Algorithm());
                    break;

                case ChecksumAlgorithm.ES_Algorithm3:
                    minimumLength = 9;
                    if (vatNumber.FormattedVatNumber.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    if (result.ValidationErrors.Count > 0)
                    {
                        result.ValidationErrors.Add(ValidationErrors.ChecksumError);
                        return;
                    }

                    result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(2).ValidateES3Algorithm());
                    break;

                case ChecksumAlgorithm.DK_Algorithm:
                    minimumLength = 8;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateDKAlgorithm());
                    break;

                case ChecksumAlgorithm.AT_Algorithm:
                    minimumLength = 8;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateATAlgorithm());
                    break;

                case ChecksumAlgorithm.JP_Algorithm:
                    minimumLength = 12;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateJPAlgorithm());
                    break;

                case ChecksumAlgorithm.CN_Algorithm:
                    minimumLength = 18;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateCNAlgorithm());
                    break;

                case ChecksumAlgorithm.TR_Algorithm:
                    minimumLength = 10;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateTRAlgorithm());
                    break;

                case ChecksumAlgorithm.SE_Algorithm:
                    minimumLength = 10;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateSEAlgorithm());
                    break;

                case ChecksumAlgorithm.NO_Algorithm:
                    minimumLength = 9;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateNOAlgorithm());
                    break;

                case ChecksumAlgorithm.RU_Algorithm:
                    var firstTypeLength = 10;
                    var secondTypeLength = 12;
                    if (digitPart.Length != firstTypeLength &&
                        digitPart.Length != secondTypeLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, firstTypeLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateRUAlgorithm());
                    break;

                case ChecksumAlgorithm.NZ_Algorithm:
                    minimumLength = 8;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateNZAlgorithm());
                    break;

                case ChecksumAlgorithm.ID_Algorithm:
                    minimumLength = 12;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateIDAlgorithm());
                    break;

                case ChecksumAlgorithm.None:
                    break;
                default:
                    result.ValidationErrors.Add(ValidationErrors.UnknownChecksumAlgorithm);
                    break;
            }
        }
    }
}
