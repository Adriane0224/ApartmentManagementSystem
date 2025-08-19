namespace Property.Application.Response;

public class PropertyRegistrationResponse
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; } = string.Empty;
    public Guid? BuildingId { get; init; }
}
