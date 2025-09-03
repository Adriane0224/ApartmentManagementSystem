namespace Property.Application.Errors
{
    public class DomainException(string message) : RankException(message)
    {
    }
}