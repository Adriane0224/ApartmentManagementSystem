using FluentResults;
using MediatR;
using Ownership.Application.Response;

namespace Ownership.Application.Commands
{
    public class OwnerCommands
    {
        public record CreateOwnerCommand(string Name, string? Email, string? Phone)
            : IRequest<Result<OwnerResponse>>;

        public record UpdateOwnerCommand(Guid OwnerId, string? Name, string? Email, string? Phone)
            : IRequest<Result<OwnerResponse>>;

        public record AssignOwnerToUnitCommand(Guid OwnerId, Guid UnitId)
            : IRequest<Result<OwnerUnitResponse>>;

        public record UnassignOwnerFromUnitCommand(Guid UnitId)
            : IRequest<Result>;
    }
}
