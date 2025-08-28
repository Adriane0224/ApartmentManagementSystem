using AutoMapper;
using Directory.Application.Response;
using Directory.Domain.Entities;

namespace Directory.Infrastructure.MappingProfiles
{
    public sealed class TenantMappingProfile : Profile
    {
        public TenantMappingProfile()
        {
            CreateMap<Tenant, TenantResponse>()
                .ForMember(d => d.id, opt => opt.MapFrom(s => s.Id.Value))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.PhoneNumber));
        }
    }
}
