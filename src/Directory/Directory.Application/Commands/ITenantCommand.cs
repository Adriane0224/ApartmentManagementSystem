using Identity.Application.Response;

namespace Directory.Application.Commands
{
    public interface ITenantCommand
    {
        Task<TenantRegistrationResponse> RegisterAsync(string name, string email, string? phoone, CancellationToken cancellationToken);



    }
}
