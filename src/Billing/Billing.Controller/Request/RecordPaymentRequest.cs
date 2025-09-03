using System.ComponentModel.DataAnnotations;

namespace Billing.Controller.Request;

public sealed class RecordPaymentRequest
{
    [Required] public Guid InvoiceId { get; set; }
    [Required] public Guid PayerId { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
    [Required] public DateTime ReceivedAt { get; set; }

    [Required, MaxLength(50)]
    public string Method { get; set; } = "";

    [MaxLength(100)]
    public string? Reference { get; set; } = "";
}
