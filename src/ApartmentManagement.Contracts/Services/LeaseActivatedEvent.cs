using ApartmentManagement.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ApartmentManagement.Contracts.Services
{
    public class LeaseActivatedEvent : IIntegrationEvent, INotification
    {
        public Guid ApartmentId { get; }

        public LeaseActivatedEvent(Guid apartmentId)
        {
            ApartmentId = apartmentId;
        }
    }
}
