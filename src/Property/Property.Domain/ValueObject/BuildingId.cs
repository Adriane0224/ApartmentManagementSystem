namespace Property.Domain.ValueObject
{
    public record BuildingId(Guid Value)
    {
        public static BuildingId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();
    }
}
