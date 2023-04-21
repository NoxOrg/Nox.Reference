using AutoMapper;
using Nox.Reference.Abstractions;

namespace Nox.Reference.Data.World.Mappings
{
    internal class CurrencyMapping : Profile
    {
        public CurrencyMapping()
        {
            CreateMap<ICurrencyInfo, Currency>()
                .ForMember(x => x.MinorUnit, x => x.MapFrom(t => t.Units.MinorCurrencyUnit))
                .ForMember(x => x.MajorUnit, x => x.MapFrom(t => t.Units.MajorCurrencyUnit));

            CreateMap<ICurrencyUsage, CurrencyUsage>()
                .ForMember(x => x.Frequent, x => x.MapFrom(t => t.Frequent.Select(u => new CurrencyFrequentUsage
                {
                    Name = u
                }).ToList()))
                .ForMember(x => x.Rare, x => x.MapFrom(t => t.Rare.Select(u => new CurrencyRareUsage
                {
                    Name = u
                }).ToList())
                );

            CreateMap<IMinorCurrencyUnit, MinorCurrencyUnit>();
            CreateMap<IMajorCurrencyUnit, MajorCurrencyUnit>();

            CreateProjection<Currency, CurrencyInfo>()
                .ForMember(x => x.Units, x => x.MapFrom(t => new CurrencyUnitInfo
                {
                    MinorCurrencyUnit = new MinorCurrencyUnitInfo
                    {
                        MajorValue = t.MinorUnit.MajorValue,
                        Name = t.MinorUnit.Name,
                        Symbol = t.MinorUnit.Symbol
                    },
                    MajorCurrencyUnit = new MajorCurrencyUnitInfo
                    {
                        Name = t.MajorUnit.Name,
                        Symbol = t.MajorUnit.Symbol
                    },
                }));

            CreateProjection<MajorCurrencyUnit, MajorCurrencyUnitInfo>();
            CreateProjection<MinorCurrencyUnit, MinorCurrencyUnitInfo>();

            CreateProjection<CurrencyUsage, CurrencyUsageInfo>()
                .ForMember(x => x.Frequent, x => x.MapFrom(t => t.Frequent.Select(x => x.Name).ToList()))
                .ForMember(x => x.Rare, x => x.MapFrom(t => t.Rare.Select(x => x.Name).ToList()));

            CreateMap<CurrencyUsage, ICurrencyUsage>()
                .As<CurrencyUsageInfo>();

            CreateMap<MajorCurrencyUnit, IMajorCurrencyUnit>()
                .As<MajorCurrencyUnitInfo>();

            CreateMap<MinorCurrencyUnit, IMinorCurrencyUnit>()
                .As<MinorCurrencyUnitInfo>();
        }
    }
}