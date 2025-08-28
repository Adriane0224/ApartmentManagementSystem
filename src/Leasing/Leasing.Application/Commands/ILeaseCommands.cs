using FluentResults;
using Leasing.Application.Response;

namespace Leasing.Application.Commands
{
    public interface ILeaseCommands
    {
        Task<Result<LeaseResponse>> CreateAsync(Guid apartmentId, Guid tenantId,
            DateOnly start, DateOnly end, decimal monthlyRent, decimal deposit, CancellationToken cancellationToken);

        Task<Result> TerminateAsync(Guid leaseId, DateOnly terminationDate, CancellationToken cancellationToken);
    }
}
