using Property.Domain.ValueObject;

namespace Property.Domain.Entities
{
    public class ApartmentUnit
    {
        // Properties
        public ApartmentId Id { get; private set; } = null!;
        public string? Description { get; set; }
        public string Unit { get; private set; }
        public int Floor { get; private set; }
        public UnitStatus Status { get; private set; }
        public string? Owner { get; set; }
        public string? Tenant { get; set; }

        protected ApartmentUnit() { }
        public enum UnitStatus
        {
            Available,
            Occupied,
            Maintenance
        }

        // Factory method to create a new ApartmentUnit
        public static ApartmentUnit Create(string unit, int floor, string description)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException("Unit cannot be null or empty.", nameof(unit));
            }

            return new ApartmentUnit
            {
                Id = new ApartmentId(Guid.NewGuid()), 
                Unit = unit,
                Status = UnitStatus.Available,
                Floor = floor,
                Description = description
                
            };
        }

        // Update method for changing unit details, such as unit and status
        public void UpdateUnitDetails(string unit, UnitStatus status)
        {
            if (string.IsNullOrWhiteSpace(unit))
            {
                throw new ArgumentException("Unit cannot be null or empty.", nameof(unit));
            }

            if (!Enum.IsDefined(typeof(UnitStatus), status))
            {
                throw new ArgumentException("Invalid status value.", nameof(status));
            }

            Unit = unit;
            Status = status;
        }
    }
}
