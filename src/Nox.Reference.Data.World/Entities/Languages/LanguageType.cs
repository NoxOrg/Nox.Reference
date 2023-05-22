using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

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