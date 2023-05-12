using System.Text.Json.Serialization;

namespace Nox.Reference.Data.World;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VerificationApi
{
    None,
    EuropeVies,
    GSTIN,
}