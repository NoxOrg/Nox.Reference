using AutoMapper;

namespace Nox.Reference.Data.World.Mappings
{
    internal class PhoneNumberMapping : Profile
    {
        public PhoneNumberMapping()
        {
            CreateMap<PhoneCarrierInfo, PhoneCarrier>()
                .ForMember(x => x.PhoneNumberCarriers,
                    x => x.MapFrom(t => t.PhoneNumbers.Select(x =>
                    new CarrierPhoneNumber
                    {
                        PhoneNumber = x
                    })));
        }
    }
}