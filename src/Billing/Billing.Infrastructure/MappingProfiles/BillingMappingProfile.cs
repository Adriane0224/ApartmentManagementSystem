using AutoMapper;
using Billing.Application.Response;
using Billing.Domain.Entities;

namespace Billing.Infrastructure.MappingProfiles
{
    public class BillingMappingProfile : Profile
    {
        public BillingMappingProfile()
        {
            CreateMap<RentInvoice, InvoiceResponse>()
                .ForMember(d => d.Id, m => m.MapFrom(s => s.Id.Value))
                .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));

            CreateMap<Payment, PaymentResponse>();
        }
    }
}
