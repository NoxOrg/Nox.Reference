using AutoMapper;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Data.Mappings
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

            CreateProjection<Currency, CurrencyInfo>();

            CreateProjection<Currency, ICurrencyInfo>();

            CreateProjection<MajorCurrencyUnit, MajorCurrencyUnitInfo>();
            CreateProjection<MinorCurrencyUnit, MinorCurrencyUnitInfo>();

            CreateProjection<MajorCurrencyUnit, IMajorCurrencyUnit>();
            CreateProjection<MinorCurrencyUnit, IMinorCurrencyUnit>();

            CreateProjection<CurrencyUsage, CurrencyUsageInfo>();

            CreateProjection<CurrencyUsage, ICurrencyUsage>()
                .ForMember(x => x.Frequent, x => x.MapFrom(t => t.Frequent.Select(x => x.Name).ToList()))
                .ForMember(x => x.Rare, x => x.MapFrom(t => t.Rare.Select(x => x.Name).ToList()));
        }
    }
}