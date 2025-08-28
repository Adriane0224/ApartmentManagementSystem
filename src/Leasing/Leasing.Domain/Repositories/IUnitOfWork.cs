namespace Leasing.Domain.Repositories
{
    public interface IUnitOfWork
    {
        ILeaseRepository Leases { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
