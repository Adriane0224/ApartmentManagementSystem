using FluentResults;

namespace Billing.Application.Errors
{
    public class InvalidPeriodError(string message) : Error(message)
    {
    }
}
