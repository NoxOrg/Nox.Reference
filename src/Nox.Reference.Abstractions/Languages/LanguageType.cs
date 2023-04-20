using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.Languages
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LanguageType
    {
        Unknown,
        Living,
        Special,
        Extinct,
        Constructed,
        Ancient,
        Historical
    }
}
