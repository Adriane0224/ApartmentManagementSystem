using FluentResults;
using Property.Application.Response;

namespace Property.Application.Commands
{
    public interface IApartmentCommands
    {
        Task<Result<ApartmentResponse>> AddApartmentAsync(string unit, int floor, CancellationToken cancellationToken);
        public Task<Result> DeleteApartmentAsync(string id, CancellationToken cancellationToken);
        public Task<Result> OccupyApartmentAsync(Guid id, CancellationToken cancellationToken);
        public Task<Result> VacantApartmentByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<Result> UnderMaintenanceApartmentAsync(string unit, CancellationToken cancellationToken);
    }
}
