using ApartmentManagement.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagement.Contracts.Services
{
    public record LeaseTerminatedIntegrationEvent(Guid ApartmentId)
        : IIntegrationEvent, INotification;
}
