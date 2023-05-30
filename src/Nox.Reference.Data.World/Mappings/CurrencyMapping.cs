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
            CreateMap<Currency, CurrencyInfo>()
                .ForMember(x => x.Units, x => x.MapFrom(x => new CurrencyUnitInfo
                {
                    MajorCurrencyUnit = new MajorCurrencyUnitInfo
                    {
                        Name = x.MajorUnit.Name,
                        Symbol = x.MajorUnit.Symbol
                    },
                    MinorCurrencyUnit = new MinorCurrencyUnitInfo
                    {
                        Name = x.MinorUnit.Name,
                        Symbol = x.MinorUnit.Symbol,
                        MajorValue = x.MinorUnit.MajorValue
                    },
                }));

            CreateMap<CurrencyUsageInfo, CurrencyUsage>()
                .ForMember(x => x.Frequent, x => x.MapFrom(t => t.Frequent.Select(u => new CurrencyFrequentUsage
                {
                    Name = u
                }).ToList()))
                .ForMember(x => x.Rare, x => x.MapFrom(t => t.Rare.Select(u => new CurrencyRareUsage
                {
                    Name = u
                }).ToList()));
            CreateMap<CurrencyUsage, CurrencyUsageInfo>()
                .ForMember(x => x.Frequent, x => x.MapFrom(t => t.Frequent.Select(u => u.Name).ToList()))
                .ForMember(x => x.Rare, x => x.MapFrom(t => t.Rare.Select(u => u.Name).ToList()));

            CreateMap<MinorCurrencyUnitInfo, MinorCurrencyUnit>().ReverseMap();
            CreateMap<MajorCurrencyUnitInfo, MajorCurrencyUnit>().ReverseMap();
        }
    }
}