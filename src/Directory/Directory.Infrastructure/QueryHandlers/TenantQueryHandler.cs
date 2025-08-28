using AutoMapper;
using AutoMapper.QueryableExtensions;
using Directory.Application.Queries;
using Directory.Application.Response;
using Directory.Domain.ValueObject;
using Directory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Directory.Infrastructure.QueryHandlers
{
    public class TenantQueries : ITenantQueries
    {
        private readonly DirectoryDbContext _context;
        private readonly IMapper _mapper;

        public TenantQueries(DirectoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TenantResponse>> GetAllAsync()
        {
            return await _context.Tenants
                .AsNoTracking()
                .ProjectTo<TenantResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<TenantResponse?> GetTenantByIdAsync(Guid id)
        {
            return await _context.Tenants
                .AsNoTracking()
                .Where(t => t.Id == new TenantId(id))
                .ProjectTo<TenantResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}
