using System.Text.Json;

namespace Nox.Reference.Common
{
    public static class NoxReferenceJsonSerializer
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static string Serialize<TType>(TType value)
        {
            return JsonSerializer.Serialize(value, _jsonOptions);
        }
    }
}