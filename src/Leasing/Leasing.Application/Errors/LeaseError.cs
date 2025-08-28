namespace Leasing.Application.Errors
{
    public class LeaseError
    {
        public static string Overlap => "Overlapping lease exists for this apartment.";
        public static string NotFound => "Lease not found.";
        public static string ApartmentNotAvailable => "Apartment is not available.";
        public static string InvalidTenant => "Tenant not found.";
        public static string InvalidApartment => "Apartment not found.";
    }
}
