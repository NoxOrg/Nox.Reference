using System.Text.Json.Serialization;

namespace Nox.Reference.GetData.CliCommands.Languages
{
    public class LanguageNativeNameInfo
    {
        [JsonPropertyName("code")] public string Code { get; set; } = string.Empty;
        [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;
        [JsonPropertyName("nativeName")] public string NativeName { get; set; } = string.Empty;
    }
}
