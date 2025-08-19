using Property.Domain.Entities;
using Property.Domain.Repositories;
using Property.Domain.ValueObject;
using Property.Infrastructure.Data;

namespace Directory.Infrastructure.Data.Repositories
{
    public class PropertyRepository : IBuildingRepository
    {
        private readonly PropertyDbContext _context;

        public PropertyRepository(PropertyDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Building building, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Building?> GetByIdAsync(BuildingId id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
