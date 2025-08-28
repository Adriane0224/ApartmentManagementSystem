using Property.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApartmentDbContext _context;
        private readonly IApartmentRepository _apartmentRepository;

        public UnitOfWork(ApartmentDbContext context, IApartmentRepository apartmentRepository)
        {
            _context = context;
            _apartmentRepository = apartmentRepository;
        }
        public IApartmentRepository Apartments => _apartmentRepository;

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
