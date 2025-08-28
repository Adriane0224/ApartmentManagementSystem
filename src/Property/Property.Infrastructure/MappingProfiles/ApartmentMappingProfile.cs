using AutoMapper;
using Property.Application.Response;
using Property.Domain.Entities;

namespace Property.Infrastructure.MappingProfiles
{
    public class ApartmentMappingProfile : Profile
    {
        public ApartmentMappingProfile()
        {
            CreateMap<ApartmentUnit, ApartmentResponse>()
                .ForMember(p => p.Id, options => options.MapFrom(p => p.Id.Value));
        }
    }
}
