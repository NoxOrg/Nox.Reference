using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VerificationApi
    {
        None,
        EuropeVies,
        GSTIN,
    }
}
