using AutoMapper;
using Nox.Reference.Abstractions.Currencies;

namespace Nox.Reference.Data.Mappings
{
    internal class CurrencyMapping : Profile
    {
        public CurrencyMapping()
        {
            CreateMap<ICurrencyInfo, Currency>()
                .ReverseMap();

            CreateMap<ICurrencyUsage, CurrencyUsage>()
                .ForMember(x => x.Frequent, x => x.MapFrom(t => string.Join(",", t.Frequent)))
                .ForMember(x => x.Rare, x => x.MapFrom(t => string.Join(",", t.Rare)));

            CreateMap<ICurrencyUnit, CurrencyUnit>()
               .ForMember(x => x.MajorValue, x => x.MapFrom(t => t.MinorCurrencyUnit.MajorValue))
               .ForMember(x => x.MinorName, x => x.MapFrom(t => t.MinorCurrencyUnit.Name))
               .ForMember(x => x.MajorName, x => x.MapFrom(t => t.MajorCurrencyUnit.Name))
               .ForMember(x => x.MinorSymbol, x => x.MapFrom(t => t.MinorCurrencyUnit.Symbol))
               .ForMember(x => x.MajorSymbol, x => x.MapFrom(t => t.MajorCurrencyUnit.Symbol));
        }
    }
}