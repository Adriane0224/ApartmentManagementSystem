using FluentResults;

namespace Borrowing.Application.Errors
{
    public class OverlapError(string message) : Error(message)
    {
    }
}
