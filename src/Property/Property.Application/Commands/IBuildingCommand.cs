using Property.Application.Response;

namespace Property.Application.Commands;
public interface IBuildingCommand
{
    Task<PropertyRegistrationResponse> CreateAsync(string name, CancellationToken cancellationToken);
}
