using Property.Application.Response;

namespace Property.Application.Commands;

public class BuildingCommands : IBuildingCommand
{
    private readonly IBuildingRepository _repo;
    public BuildingCommand(IBuildingRepository repo) => _repo = repo;

    public async Task<PropertyRegistrationResponse> CreateAsync(string name, CancellationToken cancellationToken)
    {
        return await _repo.CreateAsync(name, cancellationToken);

    }
}
