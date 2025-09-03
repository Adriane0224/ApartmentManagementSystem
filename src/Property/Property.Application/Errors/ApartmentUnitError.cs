using FluentResults;

namespace Property.Application.Errors
{
    public class ApartmentUnitError(string message) : Error(message)
    {
    }
}
