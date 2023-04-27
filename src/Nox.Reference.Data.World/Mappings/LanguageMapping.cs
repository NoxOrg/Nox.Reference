using AutoMapper;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings
{
    internal class LanguageMapping : Profile
    {
        public LanguageMapping()
        {
            //In
            CreateMap<LanguageInfoYaml, LanguageInfo>()
                .ForMember(x => x.Name, x => x.MapFrom(t => t.EnglishName));

            CreateMap<LanguageInfo, Language>();
            CreateMap<LanguageTranslationInfo, LanguageTranslation>();

            //Out
            CreateProjection<Language, LanguageInfo>();
            CreateProjection<LanguageTranslation, LanguageTranslationInfo>();

            CreateMap<LanguageTranslation, ILanguageTranslation>()
                .As<LanguageTranslationInfo>();
        }
    }
}