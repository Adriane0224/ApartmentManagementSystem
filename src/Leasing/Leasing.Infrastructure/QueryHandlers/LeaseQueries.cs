using AutoMapper;
using AutoMapper.QueryableExtensions;
using Leasing.Application.Queries;
using Leasing.Application.Response;
using Leasing.Domain.Entities;
using Leasing.Domain.ValueObject;
using Leasing.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Leasing.Infrastructure.QueryHandlers
{
    public class LeaseQueries : ILeaseQueries
    {
        private readonly LeasingDbContext _db;
        private readonly IMapper _mapper;
        public LeaseQueries(LeasingDbContext db, IMapper mapper) { _db = db; _mapper = mapper; }

        public Task<LeaseResponse?> GetByIdAsync(Guid id, CancellationToken ct)
            => _db.Leases.AsNoTracking()
                 .Where(l => l.Id == new LeaseId(id))
                .ProjectTo<LeaseResponse>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(ct);

        public Task<List<LeaseResponse>> GetActiveByApartmentAsync(Guid apartmentId, CancellationToken ct)
            => _db.Leases.AsNoTracking()
                .Where(l => l.ApartmentId == apartmentId && l.Status == Lease.LeaseStatus.Active)
                .ProjectTo<LeaseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);

        public Task<List<LeaseResponse>> GetActiveAsync(CancellationToken ct)
            => _db.Leases.AsNoTracking()
                .Where(l => l.Status == Lease.LeaseStatus.Active)
                .ProjectTo<LeaseResponse>(_mapper.ConfigurationProvider)
                .ToListAsync(ct);
    }
}
