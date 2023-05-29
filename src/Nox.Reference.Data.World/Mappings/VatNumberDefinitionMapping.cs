using AutoMapper;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World
{
    internal class VatNumberDefinitionMapping : Profile
    {
        public VatNumberDefinitionMapping()
        {
            // IN
            CreateMap<VatNumberDefinitionInfo, VatNumberDefinition>()
                .ForMember(x => x.ValidationRules, x => x.MapFrom(t => t.Validations))
                .ReverseMap();

            CreateMap<ValidationInfo, VatNumberValidationRule>()
                .ReverseMap();

            CreateMap<ChecksumInfo, Checksum>()
                .ForMember(x => x.Weights, x => x.MapFrom(t => string.Join(",", t.Weights ?? new List<int>())));
            CreateMap<Checksum, ChecksumInfo>()
                .ForMember(x => x.Weights, x => x.MapFrom(t => t.GetWeights().ToList()));
        }
    }
}