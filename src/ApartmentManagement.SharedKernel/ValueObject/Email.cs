namespace ApartmentManagement.SharedKernel.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email is required.", nameof(value));
        var normalized = value.Trim();
        if (!normalized.Contains('@') || normalized.StartsWith("@") || normalized.EndsWith("@"))
            throw new ArgumentException("Invalid email format.", nameof(value));
        return new Email(normalized.ToLowerInvariant());
    }

    public override string ToString() => Value;
}
