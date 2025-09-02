namespace Billing.Domain.Ports
{
    public interface ILeasingReadPort
    {
        Task<List<LeaseDto>> GetActiveLeasesAsync(CancellationToken ct);
    }

    public sealed class LeaseDto
    {
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid TenantId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public string Status { get; set; } = "Active";
    }
}
