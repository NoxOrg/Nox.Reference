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
                .ForMember(x => x.Name, x => x.MapFrom(t => t.EnglishName))
                .ReverseMap();

            CreateMap<LanguageInfo, Language>();

            CreateMap<Language, LanguageInfo>()
                .ForMember(x => x.Countries, x => x.MapFrom(x => x.Countries.Select(x => x.Code)));

            CreateMap<LanguageTranslationInfo, LanguageTranslation>()
                .ReverseMap();
        }
    }
}