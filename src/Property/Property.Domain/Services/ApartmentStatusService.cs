using Property.Domain.Entities;

namespace Property.Domain.Services
{
    public class ApartmentStatusService
    {
        public ApartmentUnit Occupy(ApartmentUnit unit)
        {
            unit.UpdateUnitDetails(unit.Unit, ApartmentUnit.UnitStatus.Occupied);
            return unit;
        }

        public ApartmentUnit MarkAsVacant(ApartmentUnit unit)
        {
            unit.UpdateUnitDetails(unit.Unit, ApartmentUnit.UnitStatus.Available);
            return unit;
        }

        public ApartmentUnit MarkAsUnderMaintenance(ApartmentUnit unit)
        {
            unit.UpdateUnitDetails(unit.Unit, ApartmentUnit.UnitStatus.Maintenance);
            return unit;
        }
    }
}
