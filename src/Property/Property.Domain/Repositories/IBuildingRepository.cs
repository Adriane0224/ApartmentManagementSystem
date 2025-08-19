using Property.Domain.Entities;
using Property.Domain.ValueObject;

namespace Property.Domain.Repositories;

public interface IBuildingRepository
{
    Task<Building?> GetByIdAsync(BuildingId id, CancellationToken cancellationToken );
    Task AddAsync(Building building, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
