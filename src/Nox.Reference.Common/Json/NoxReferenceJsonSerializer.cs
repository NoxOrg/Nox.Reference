using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nox.Reference.Common
{
    public static class NoxReferenceJsonSerializer
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public static string Serialize<TType>(TType value)
        {
            return JsonSerializer.Serialize(value, _jsonOptions);
        }
    }
}