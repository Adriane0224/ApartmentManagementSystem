using AutoMapper;
using Leasing.Application.Response;
using Leasing.Domain.Entities;

namespace Leasing.Infrastructure.MappingProfiles
{
    public class LeaseMappingProfile : Profile
    {
        public LeaseMappingProfile()
        {
            CreateMap<Lease, LeaseResponse>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id.Value))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()));
        }
    }
}
