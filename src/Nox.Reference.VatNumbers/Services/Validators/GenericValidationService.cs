using Nox.Reference.Abstractions.VatNumbers;
using Nox.Reference.Shared;
using Nox.Reference.VatNumbers.Constants;
using Nox.Reference.VatNumbers.Extension;
using System.Text.RegularExpressions;

namespace Nox.Reference.VatNumbers.Services.Validators
{
    public class GenericValidationService : VatValidationServiceBase
    {
        public override ValidationResult ValidateVatNumber(IVatNumberInfo vatNumber)
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

            return result;
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

            if (decimal.Parse(digitPart) <= 0)
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

            if (vatNumber.Validations!.Count == 1)
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
                    minimumLength = 10;
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
                case ChecksumAlgorithm.ModAndSubstractItaly:
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
                case ChecksumAlgorithm.MexicanAlgorithm:
                    // length is checked inside 

                    result.ValidationErrors.AddRange(vatNumber.OriginalVatNumber.ValidateMexicanAlgorithm());
                    break;
                case ChecksumAlgorithm.GermanAlgorithm:
                    minimumLength = 9;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateGermanAlgorithm());
                    break;
                case ChecksumAlgorithm.FrenchAlgorithm:
                    minimumLength = 2;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateFrenchAlgorithm());
                    break;
                case ChecksumAlgorithm.ColombianAlgorithm:
                    minimumLength = 1;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateColombianAlgorithm());
                    break;
                case ChecksumAlgorithm.AustralianAlgorithm:
                    minimumLength = 9;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateAustralianAlgorithm());
                    break;
                case ChecksumAlgorithm.BelgianAlgorithm:
                    minimumLength = 9;
                    if (digitPart.Length < minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.MinimumNumbericLengthError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateBelgianAlgorithm());
                    break;
                case ChecksumAlgorithm.BrazilianAlgorithm:
                    minimumLength = 14;
                    if (digitPart.Length != minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateBralizianAlgorithm());
                    break;

                case ChecksumAlgorithm.CanadianAlgorithm:
                    minimumLength = 9;
                    if (digitPart.Length != minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateCanadianAlgorithm());
                    break;

                case ChecksumAlgorithm.SwissAlgorithm:
                    minimumLength = 9;
                    if (digitPart.Length != minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateSwissAlgorithm());
                    break;

                case ChecksumAlgorithm.BritishAlgorithm:
                    minimumLength = 9;
                    if (digitPart.Length != minimumLength)
                    {
                        result.ValidationErrors.Add(string.Format(ValidationErrors.LengthShouldEqualError, minimumLength));
                        return;
                    }

                    result.ValidationErrors.AddRange(digitPart.ValidateBritishAlgorithm());
                    break;

                case ChecksumAlgorithm.Spanish1Algorithm:
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

                    result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(3).ValidateSpanish1Algorithm());
                    break;

                case ChecksumAlgorithm.Spanish2Algorithm:
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

                    result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(3).ValidateSpanish2Algorithm());
                    break;

                case ChecksumAlgorithm.Spanish3Algorithm:
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

                    result.ValidationErrors.AddRange(vatNumber.FormattedVatNumber.Substring(2).ValidateSpanish3Algorithm());
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
