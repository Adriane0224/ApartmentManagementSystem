using MediatR;
using Microsoft.EntityFrameworkCore;
using Ownership.Domain.DomainEvents;
using Property.Infrastructure.Data;   
using Property.Infrastructure.ReadModels;

namespace Property.Infrastructure.EventHandlers 
{
    public sealed class OwnershipEventsHandler :
        INotificationHandler<OwnerAssignedToUnitDomainEvent>,
        INotificationHandler<OwnerUnassignedFromUnitDomainEvent>
    {
        private readonly ApartmentDbContext _db;  

        public OwnershipEventsHandler(ApartmentDbContext db) => _db = db;

        public async Task Handle(OwnerAssignedToUnitDomainEvent e, CancellationToken ct)
        {
            var row = await _db.UnitOwners.FirstOrDefaultAsync(x => x.UnitId == e.UnitId, ct);
            if (row is null)
            {
                row = new UnitOwnerView
                {
                    UnitId = e.UnitId,
                    OwnerId = e.OwnerId,
                    Name = e.OwnerName,
                    Email = e.Email,
                    Phone = e.Phone
                };
                await _db.UnitOwners.AddAsync(row, ct);
            }
            else
            {
                row.OwnerId = e.OwnerId;
                row.Name = e.OwnerName;
                row.Email = e.Email;
                row.Phone = e.Phone;
                _db.UnitOwners.Update(row);
            }

            await _db.SaveChangesAsync(ct);
        }

        public async Task Handle(OwnerUnassignedFromUnitDomainEvent e, CancellationToken ct)
        {
            var row = await _db.UnitOwners.FirstOrDefaultAsync(x => x.UnitId == e.UnitId, ct);
            if (row is not null)
            {
                _db.UnitOwners.Remove(row);
                await _db.SaveChangesAsync(ct);
            }
        }
    }
}
