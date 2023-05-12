using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LanguageScope
{
    Unknown,
    Individual,
    MacroLanguage,
    Special
}