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
            return Task.CompletedTask; 
        }

        public Task<List<ApartmentUnit>> GetAllAsync()
        {
            // for read-only list queries
            return _context.Apartments.AsNoTracking().ToListAsync();
        }
        public Task UpdateAsync(ApartmentUnit apartmentUnit)
        {
            _context.Apartments.Update(apartmentUnit);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ApartmentUnit apartmentUnit, CancellationToken ct)
        {
            _context.Apartments.Update(apartmentUnit);
            return Task.CompletedTask;
        }

        public Task<ApartmentUnit?> GetByIdForUpdateAsync(ApartmentId id, CancellationToken cancellationToken)
        {
            return _context.Apartments
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public Task<ApartmentUnit?> GetByIdAsync(Guid apartmentId, CancellationToken cancellationToken)
        {
            return _context.Apartments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id.Value == apartmentId, cancellationToken);
        }
    }
}
