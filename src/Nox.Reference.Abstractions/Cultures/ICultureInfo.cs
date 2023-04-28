namespace Nox.Reference.Abstractions.Cultures
{
    public interface ICultureInfo
    {
        public string Id { get; }
        public string FormalName { get; }
        public string NativeName { get; }
        public string? CommonName { get; }

        public string Language { get; }
        public string Country { get; }
        public string DisplayName { get; }
        public string DisplayNameWithDialect { get; }
        public string CharacterOrientation { get; }
        public string LineOrientation { get; }
        public string? LanguageIso_639_2t { get; }

        public INumberFormatInfo NumberFormat { get; }
        public IDateFormatInfo DateFormat { get; }
    }
}
