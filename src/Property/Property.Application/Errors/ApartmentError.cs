using FluentResults;

namespace Property.Application.Errors
{
    public class ApartmentError(string message) : Error(message) 
    {
    }
}
