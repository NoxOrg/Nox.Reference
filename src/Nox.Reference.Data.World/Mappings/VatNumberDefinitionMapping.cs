using AutoMapper;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World
{
    internal class VatNumberDefinitionMapping : Profile
    {
        public VatNumberDefinitionMapping()
        {
            // IN
            CreateMap<VatNumberDefinitionInfo, VatNumberDefinition>()
                .ForMember(x => x.ValidationRules, x => x.MapFrom(t => t.Validations));

            CreateMap<ValidationInfo, VatNumberValidationRule>();
            CreateMap<IChecksumInfo, Checksum>().ForMember(x => x.Weights, x => x.MapFrom(t => string.Join(",", t.Weights ?? new List<int>())));

            // OUT
            CreateProjection<VatNumberDefinition, VatNumberDefinitionInfo>()
                .ForMember(x => x.Validations, x => x.MapFrom(t => t.ValidationRules));

            CreateProjection<VatNumberValidationRule, ValidationInfo>()
                .ForMember(x => x.Checksum, x => x.MapFrom(t => t.Checksum == null ? null : new ChecksumInfo
                {
                    Algorithm = t.Checksum.Algorithm,
                    ChecksumDigit = t.Checksum.ChecksumDigit,
                    Modulus = t.Checksum.Modulus,
                    Weights = new List<int>(t.Checksum.GetWeights())
                }));

            CreateMap<VatNumberValidationRule, IValidationInfo>()
                .As<ValidationInfo>();
        }
    }
}