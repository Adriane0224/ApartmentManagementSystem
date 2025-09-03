namespace Leasing.Domain.Exception
{
    public class MonthlyRentPositiveException(string message) : DomainException(message)
    {
    }
}
