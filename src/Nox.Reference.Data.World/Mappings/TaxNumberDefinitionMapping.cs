using AutoMapper;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World
{
    internal class TaxNumberDefinitionMapping : Profile
    {
        public TaxNumberDefinitionMapping()
        {
            CreateMap<TaxNumberDefinitionInfo, TaxNumberDefinition>()
                .ForMember(x => x.ValidationRules, x => x.MapFrom(t => t.Validations))
                .ForMember(x => x.CountryCode, x => x.MapFrom(x => x.Country))
                .ForMember(x => x.Country, x => x.Ignore());
            CreateMap<TaxNumberDefinition, TaxNumberDefinitionInfo>()
                .ForMember(x => x.Validations, x => x.MapFrom(t => t.ValidationRules))
                .ForMember(x => x.Country, x => x.MapFrom(x => x.CountryCode));

            CreateMap<ValidationInfo, TaxNumberValidationRule>()
                .ReverseMap();

            // TODO: move shared entities
            CreateMap<ChecksumInfo, Checksum>()
                .ForMember(x => x.Weights, x => x.MapFrom(t => string.Join(",", t.Weights ?? new List<int>())));
            CreateMap<Checksum, ChecksumInfo>()
                .ForMember(x => x.Weights, x => x.MapFrom(t => t.GetWeights().ToList()));
        }
    }
}