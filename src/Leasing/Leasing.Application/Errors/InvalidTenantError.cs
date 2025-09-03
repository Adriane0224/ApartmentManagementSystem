using FluentResults;

namespace Borrowing.Application.Errors
{
    public class InvalidTenantError(string message) : Error(message)
    {
    }
}
