using Directory.Domain.Entities;
using Directory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Directory.Infrastructure.Data.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly DirectoryDbContext _context;

        public TenantRepository(DirectoryDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Tenant tenant)
        {
            await _context.Tenants.AddAsync(tenant);
        }

        public async Task<Tenant?> GetByEmailAsync(string email)
        {
            return await _context.Tenants
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Tenant?> GetByIdAsync(Guid id)
        {
            return await _context.Tenants.FindAsync(id);
        }

        public async Task<List<Tenant>> GetUsersAsync()
        {
            return await _context.Tenants.ToListAsync();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
