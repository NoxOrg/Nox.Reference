using AutoMapper;
using Nox.Reference.Data.World.Models;

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
        }
    }
}