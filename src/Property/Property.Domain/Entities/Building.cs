using ApartmentManagement.SharedKernel.Entities;
using Property.Domain.ValueObject;

namespace Property.Domain.Entities;

public class Building : Entity
{
    public BuildingId Id { get; private set; }
    public string Name { get; private set; } = default!;

    private Building() { }
    private Building(BuildingId id, string name) { Id = id; Name = name; }

    public static Building Create(string name) => new(BuildingId.New(), name);
}
