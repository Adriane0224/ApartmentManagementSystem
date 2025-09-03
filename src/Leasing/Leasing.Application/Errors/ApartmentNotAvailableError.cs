using FluentResults;

namespace Borrowing.Application.Errors
{
    public class ApartmentNotAvailableError(string message) : Error(message)
    {
    }
}
