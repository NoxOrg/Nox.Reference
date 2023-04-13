using System.Text.Json.Serialization;

namespace Nox.Reference.Abstractions.VatNumbers
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum VerificationApi
    {
        None,
        EuropeVies,
        GSTIN,
    }
}
