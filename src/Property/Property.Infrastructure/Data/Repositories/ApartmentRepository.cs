using Microsoft.EntityFrameworkCore;
using Property.Domain.Entities;
using Property.Domain.Repositories;
using Property.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task AddAsync(ApartmentUnit apartmentUnit, CancellationToken cancellationToken)
        {
            await _context.Apartments.AddAsync(apartmentUnit);
        }

        public async Task DeleteAsync(ApartmentUnit apartmentUnit)
        {
            _context.Apartments.Remove(apartmentUnit);
        }

        public async Task<List<ApartmentUnit>> GetAllAsync()
        {
            return await _context.Apartments.ToListAsync();
        }

        public async Task UpdateAsync(ApartmentUnit apartmentUnit)
        {
             _context.Apartments.Update(apartmentUnit);
        }

        public async Task<ApartmentUnit?> GetByIdForUpdateAsync(ApartmentId id, CancellationToken cancellationToken)
        {
            return await _context.Apartments
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<ApartmentUnit?> GetByIdAsync(Guid apartmentId, CancellationToken cancellationToken)
        {
            return await _context.Apartments
                .FirstOrDefaultAsync(a => a.Id.Value == apartmentId, cancellationToken);
        }

        public Task UpdateAsync(ApartmentUnit apartmentUnit, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
