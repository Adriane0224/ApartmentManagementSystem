namespace Billing.Domain.Repositories
{
    public interface IUnitOfWork
    {
        IInvoiceRepository Invoices { get; }
        IPaymentRepository Payments { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
        //Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
