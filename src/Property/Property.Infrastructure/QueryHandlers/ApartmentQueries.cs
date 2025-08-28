using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Property.Application.Queries;
using Property.Application.Response;
using Property.Domain.Entities;
using Property.Domain.ValueObject;
using Property.Infrastructure.Data;
using static Property.Domain.Entities.ApartmentUnit;

namespace Property.Infrastructure.QueryHandlers
{
    public class ApartmentQueries : IApartmentQueries
    {
        private readonly ApartmentDbContext _context;
        private readonly IMapper _mapper;

        public ApartmentQueries(ApartmentDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ApartmentResponse>> GetAllOccupiedApartmentsAsync()
        {
            return await _context.Apartments
            .Where(a => a.Status == UnitStatus.Occupied)
            .ProjectTo<ApartmentResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        }

        public async Task<List<ApartmentResponse>> GetAllApartmentsAsync()
        {
           return await _context.Apartments.ProjectTo<ApartmentResponse>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<ApartmentResponse>> GetAllUnderMaintenanceApartments()
        {
            return await _context.Apartments
            .Where(a => a.Status == UnitStatus.Maintenance)
            .ProjectTo<ApartmentResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<List<ApartmentResponse>> GetAllVacantApartmentsAsync()
        {
            return await _context.Apartments
                .Where(a => a.Status == ApartmentUnit.UnitStatus.Available)
                .ProjectTo<ApartmentResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ApartmentResponse?> GetApartmentByIdAsync(string? unit)
        {
            var apartment = await _context.Apartments.Where(u => u.Unit == unit).FirstOrDefaultAsync();
            if (apartment == null)
            {
                return null;
            }
            return _mapper.Map<ApartmentResponse>(apartment);
        }

        public Task<List<ApartmentResponse>> GetAllAvailableApartments()
        {
            var apartments = _context.Apartments.Where(a => a.Status == UnitStatus.Available).ToList();
            if (apartments == null)
            {
                return Task.FromResult(new List<ApartmentResponse>());
            }
            return Task.FromResult(_mapper.Map<List<ApartmentResponse>>(apartments));
        }

        public async Task<ApartmentResponse?> GetApartmentByIdAsync(Guid id)
        {
            var entity = await _context.Apartments
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == new ApartmentId(id));

            return entity is null ? null : _mapper.Map<ApartmentResponse>(entity);
        }
    }
}
