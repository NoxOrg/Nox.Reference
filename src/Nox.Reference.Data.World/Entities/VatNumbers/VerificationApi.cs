using System.Text.Json.Serialization;

namespace Nox.Reference;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VerificationApi
{
    None,
    EuropeVies,
    GSTIN,
}