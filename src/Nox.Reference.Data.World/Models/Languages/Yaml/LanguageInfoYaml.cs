using YamlDotNet.Serialization;

namespace Nox.Reference;

internal class LanguageInfoYaml
{
    [YamlMember(Alias = ":name")] public string EnglishName { get; set; } = string.Empty;
    [YamlMember(Alias = ":iso_639_1")] public string? Iso_639_1 { get; set; }
    [YamlMember(Alias = ":iso_639_3")] public string Iso_639_3 { get; set; } = string.Empty;
    [YamlMember(Alias = ":iso_639_2b")] public string? Iso_639_2b { get; set; }
    [YamlMember(Alias = ":iso_639_2t")] public string? Iso_639_2t { get; set; }
    [YamlMember(Alias = ":common")] public bool Common { get; set; }
    [YamlMember(Alias = ":type")] public LanguageType Type { get; set; }
    [YamlMember(Alias = ":scope")] public LanguageScopeYaml Scope { get; set; }
}