using FluentResults;

namespace Billing.Application.Errors
{
    public class InvoiceNotFoundError(string message) : Error(message)
    {
    }
}
