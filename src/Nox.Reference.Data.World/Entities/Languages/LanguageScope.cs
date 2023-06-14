using System.Text.Json.Serialization;

namespace Nox.Reference;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LanguageScope
{
    Unknown,
    Individual,
    MacroLanguage,
    Special
}