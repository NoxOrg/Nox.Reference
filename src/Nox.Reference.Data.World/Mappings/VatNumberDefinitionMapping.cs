using AutoMapper;

namespace Nox.Reference.Data.World
{
    internal class VatNumberDefinitionMapping : Profile
    {
        public VatNumberDefinitionMapping()
        {
            CreateMap<VatNumberDefinitionInfo, VatNumberDefinition>()
                .ForMember(x => x.ValidationRules, x => x.MapFrom(t => t.Validations))
                .ForMember(x => x.CountryCode, x => x.MapFrom(x => x.Country))
                .ForMember(x => x.Country, x => x.Ignore());
            CreateMap<VatNumberDefinition, VatNumberDefinitionInfo>()
                .ForMember(x => x.Validations, x => x.MapFrom(t => t.ValidationRules))
                .ForMember(x => x.Country, x => x.MapFrom(x => x.CountryCode));

            CreateMap<ValidationInfo, VatNumberValidationRule>()
                .ReverseMap();

            CreateMap<ChecksumInfo, Checksum>()
                .ForMember(x => x.Weights, x => x.MapFrom(t => string.Join(",", t.Weights ?? new List<int>())));
            CreateMap<Checksum, ChecksumInfo>()
                .ForMember(x => x.Weights, x => x.MapFrom(t => t.GetWeights().ToList()));
        }
    }
}