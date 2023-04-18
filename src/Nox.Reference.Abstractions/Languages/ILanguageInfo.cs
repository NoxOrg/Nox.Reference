using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.Languages
{
    public interface ILanguageInfo
    {
        public string EnglishName { get; }
        public string? Iso_639_1 { get; }
        public string Iso_639_3 { get; }
        public string? Iso_639_2b { get; }
        public string? Iso_639_2t { get; }
        public bool Common { get; }
        public LanguageType Type { get; }
        public LanguageScope Scope { get; }
        public string? GermanName { get; }
        public string? FrenchName { get; }
        public string? WikiUrl { get; }
        public string? NativeName { get; }
    }
}
