using AutoMapper;
using Nox.Reference.Data.World.Models;

namespace Nox.Reference.Data.World.Mappings
{
    internal class CurrencyMapping : Profile
    {
        public CurrencyMapping()
        {
            CreateMap<CurrencyInfo, Currency>()
                .ForMember(x => x.MinorUnit, x => x.MapFrom(t => t.Units.MinorCurrencyUnit))
                .ForMember(x => x.MajorUnit, x => x.MapFrom(t => t.Units.MajorCurrencyUnit));

            CreateMap<CurrencyUsageInfo, CurrencyUsage>()
                .ForMember(x => x.Frequent, x => x.MapFrom(t => t.Frequent.Select(u => new CurrencyFrequentUsage
                {
                    Name = u
                }).ToList()))
                .ForMember(x => x.Rare, x => x.MapFrom(t => t.Rare.Select(u => new CurrencyRareUsage
                {
                    Name = u
                }).ToList())
                );

            CreateMap<MinorCurrencyUnitInfo, MinorCurrencyUnit>();
            CreateMap<MajorCurrencyUnitInfo, MajorCurrencyUnit>();
        }
    }
}