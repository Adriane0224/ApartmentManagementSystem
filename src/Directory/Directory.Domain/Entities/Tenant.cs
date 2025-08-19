using ApartmentManagement.SharedKernel.Entities;
using Directory.Domain.DomainEvents;
using Directory.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory.Domain.Entities
{
    public class Tenant : Entity
    {
        public TenantId Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public Tenant(TenantId id, string name, string email, string phoneNumber)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static Tenant Create(string name, string email, string phone)
        {
            var tenant = new Tenant(new TenantId(Guid.NewGuid()), name, email, phone);
            tenant.RaiseDomainEvent(new TenantCreatedEvent(tenant));
            return tenant;
        }
    }
}
