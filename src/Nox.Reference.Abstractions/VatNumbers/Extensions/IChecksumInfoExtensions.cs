namespace Nox.Reference.Abstractions.VatNumbers.Extensions
{
    public static class IChecksumInfoExtensions
    {
        public static int GetCheckdigitValue(this IChecksumInfo checksumInfo,string formattedVatNumber)
        {
            if (checksumInfo.ChecksumDigit == "Last")
            {
                return formattedVatNumber[^1];
            }

            if (!int.TryParse(checksumInfo.ChecksumDigit, out var result))
            {
                // TODO: add custom exception
                throw new Exception("Failed to parse checksum digit!");
            }

            bool shouldTakeFromEnd = false;
            if (result == 0)
            {
                result = 1;
                shouldTakeFromEnd = true;
            }
            else if (result < 0)
            {
                result = Math.Abs(result);
                shouldTakeFromEnd = true;
            }

            if (shouldTakeFromEnd)
            {
                return formattedVatNumber[^result];
            }

            return formattedVatNumber[result];
        }
    }
}
