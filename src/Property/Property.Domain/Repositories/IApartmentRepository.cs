using Property.Domain.Entities;
using Property.Domain.ValueObject;

namespace Property.Domain.Repositories
{
    public interface IApartmentRepository
    {
        Task<List<ApartmentUnit>> GetAllAsync();
        Task AddAsync(ApartmentUnit apartmentUnit, CancellationToken ct);
        Task DeleteAsync(ApartmentUnit apartmentUnit);
        Task UpdateAsync(ApartmentUnit apartmentUnit, CancellationToken ct);
        Task<ApartmentUnit?> GetByIdAsync(Guid apartmentId, CancellationToken ct);
        Task<ApartmentUnit?> GetByIdForUpdateAsync(ApartmentId id, CancellationToken ct);
        Task UpdateAsync(ApartmentUnit occupied);
    }
}
