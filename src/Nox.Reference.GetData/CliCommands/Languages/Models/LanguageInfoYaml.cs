using Nox.Reference.Abstractions.Languages;
using YamlDotNet.Serialization;

namespace Nox.Reference.Languages.Models
{
    public class LanguageInfoYaml
    {
        [YamlMember(Alias = ":name")] public string EnglishName { get; set; } = string.Empty;
        [YamlMember(Alias = ":iso_639_1")] public string? Iso_639_1 { get; set; }
        [YamlMember(Alias = ":iso_639_3")] public string Iso_639_3 { get; set; } = string.Empty;
        [YamlMember(Alias = ":iso_639_2b")] public string? Iso_639_2b { get; set; }
        [YamlMember(Alias = ":iso_639_2t")] public string? Iso_639_2t { get; set; }
        [YamlMember(Alias = ":common")] public bool Common { get; set; }
        [YamlMember(Alias = ":type")] public LanguageType Type { get; set; }
        [YamlMember(Alias = ":scope")] public LanguageScopeYaml Scope { get; set; }

        public LanguageInfo ToLanguageInfo() => new LanguageInfo
        {
            EnglishName_ = EnglishName,
            Iso_639_1_ = Iso_639_1,
            Iso_639_3_ = Iso_639_3,
            Iso_639_2b_ = Iso_639_2b,
            Iso_639_2t_ = Iso_639_2t,
            Common_ = Common,
            Type_ = Type,
            Scope_ = (LanguageScope)(int)Scope
        };
    }
}