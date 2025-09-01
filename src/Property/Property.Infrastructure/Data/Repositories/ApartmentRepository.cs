using Microsoft.EntityFrameworkCore;
using Property.Domain.Entities;
using Property.Domain.Repositories;
using Property.Domain.ValueObject;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Property.Infrastructure.Data.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly ApartmentDbContext _context;

        public ApartmentRepository(ApartmentDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(ApartmentUnit apartmentUnit, CancellationToken cancellationToken)
        {
            return _context.Apartments.AddAsync(apartmentUnit, cancellationToken).AsTask();
        }

        public Task DeleteAsync(ApartmentUnit apartmentUnit)
        {
            _context.Apartments.Remove(apartmentUnit);
            return Task.CompletedTask; // actual save happens in UnitOfWork
        }

        public Task<List<ApartmentUnit>> GetAllAsync()
        {
            // Use AsNoTracking for read-only list queries
            return _context.Apartments.AsNoTracking().ToListAsync();
        }

        // Interface overload WITHOUT CancellationToken
        public Task UpdateAsync(ApartmentUnit apartmentUnit)
        {
            _context.Apartments.Update(apartmentUnit);
            return Task.CompletedTask; // actual save happens in UnitOfWork
        }

        // Interface overload WITH CancellationToken
        public Task UpdateAsync(ApartmentUnit apartmentUnit, CancellationToken ct)
        {
            _context.Apartments.Update(apartmentUnit);
            return Task.CompletedTask; // actual save happens in UnitOfWork
        }

        public Task<ApartmentUnit?> GetByIdForUpdateAsync(ApartmentId id, CancellationToken cancellationToken)
        {
            // Tracked entity, suitable for updates
            return _context.Apartments
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public Task<ApartmentUnit?> GetByIdAsync(Guid apartmentId, CancellationToken cancellationToken)
        {
            // AsNoTracking for read-only query
            return _context.Apartments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id.Value == apartmentId, cancellationToken);
        }
    }
}
