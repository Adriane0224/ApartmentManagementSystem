using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ownership.Domain.DomainEvents
{
    public record OwnerAssignedToUnitDomainEvent(
        Guid UnitId,
        Guid OwnerId,
        string OwnerName,
        string? Email,
        string? Phone
    ) : INotification;
}
