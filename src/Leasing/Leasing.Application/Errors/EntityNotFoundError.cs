using FluentResults;

namespace Borrowing.Application.Errors
{
    public class EntityNotFoundError(string message) : Error(message)
    {

    }
}
