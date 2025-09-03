using AutoMapper;
using Ownership.Domain.Entities;
using Ownership.Application.Response;

namespace Ownership.Infrastructure.MappingProfiles
{
    public class OwnershipMappingProfile : Profile
    {
        public OwnershipMappingProfile()
        {
            CreateMap<Owner, OwnerResponse>();
            CreateMap<OwnerUnit, OwnerUnitResponse>();
        }
    }
}
