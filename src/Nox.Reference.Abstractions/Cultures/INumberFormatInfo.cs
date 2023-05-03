namespace Nox.Reference.Abstractions.Cultures
{
    public interface INumberFormatInfo
    {
        public string CurrencySymbol { get; }
        public string DecimalSeparator { get; }
        public string Digit { get; }
        public string ExponentSeparator { get; }
        public string GroupingSeparator { get; }
        public string Infinity { get; }
        public string InternationalCurrencySymbol { get; }
        public string MinusSign { get; }
        public string MonetaryDecimalSeparator { get; }
        public string NotANumberSymbol { get; }
        public string PadEscape { get; }
        public string PatternSeparator { get; }
        public string Percent { get; }
        public string PerMill { get; }
        public string PlusSign { get; }
        public string SignificantDigit { get; }
        public string ZeroDigit { get; }
    }
}
