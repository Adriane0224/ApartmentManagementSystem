using AutoMapper;
using FluentResults;
using MediatR;
using Ownership.Application.Commands;
using Ownership.Application.Response;
using Ownership.Domain.Entities;
using Ownership.Domain.Repositories;
using Ownership.Domain.DomainEvents;
using static Ownership.Application.Commands.OwnerCommands;

namespace Ownership.Application.CommandHandler
{
    public sealed class OwnerCommandHandler :
        IRequestHandler<CreateOwnerCommand, Result<OwnerResponse>>,
        IRequestHandler<UpdateOwnerCommand, Result<OwnerResponse>>,
        IRequestHandler<AssignOwnerToUnitCommand, Result<OwnerUnitResponse>>,
        IRequestHandler<UnassignOwnerFromUnitCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OwnerCommandHandler(IUnitOfWork uow, IMapper mapper, IMediator mediator)
        {
            _uow = uow;
            _mapper = mapper;
            _mediator = mediator;
        }

        // CREATE
        public async Task<Result<OwnerResponse>> Handle(CreateOwnerCommand r, CancellationToken ct)
        {
            // If your domain has a factory, use it; otherwise new up the entity.
            var owner = Owner.Create(r.Name, r.Email, r.Phone); // <-- or: new Owner { Id = Guid.NewGuid(), Name = r.Name, Email = r.Email, Phone = r.Phone };

            await _uow.Owners.AddAsync(owner, ct);
            await _uow.SaveChangesAsync(ct);

            return Result.Ok(_mapper.Map<OwnerResponse>(owner));
        }

        // UPDATE
        public async Task<Result<OwnerResponse>> Handle(UpdateOwnerCommand r, CancellationToken ct)
        {
            // NOTE: adjust property name (r.OwnerId vs r.Id) to match your command
            var owner = await _uow.Owners.GetByIdAsync(r.OwnerId, ct);
            if (owner is null) return Result.Fail("Owner not found.");

            // If your domain has a method, call it; otherwise set properties
            owner.Update(r.Name, r.Email, r.Phone); // <-- or: owner.Name = r.Name; owner.Email = r.Email; owner.Phone = r.Phone;

            await _uow.Owners.UpdateAsync(owner, ct);
            await _uow.SaveChangesAsync(ct);

            return Result.Ok(_mapper.Map<OwnerResponse>(owner));
        }

        // ASSIGN
        public async Task<Result<OwnerUnitResponse>> Handle(AssignOwnerToUnitCommand r, CancellationToken ct)
        {
            await _uow.OwnerUnits.RemoveByUnitAsync(r.UnitId, ct);

            var link = OwnerUnit.Assign(r.OwnerId, r.UnitId);
            await _uow.OwnerUnits.AssignAsync(link, ct);
            await _uow.SaveChangesAsync(ct);

            var owner = await _uow.Owners.GetByIdAsync(r.OwnerId, ct);
            if (owner is not null)
            {
                await _mediator.Publish(
                    new OwnerAssignedToUnitDomainEvent(
                        UnitId: r.UnitId,
                        OwnerId: owner.Id,
                        OwnerName: owner.Name,
                        Email: owner.Email,
                        Phone: owner.Phone),
                    ct);
            }

            return Result.Ok(_mapper.Map<OwnerUnitResponse>(link));
        }

        // UNASSIGN
        public async Task<Result> Handle(UnassignOwnerFromUnitCommand r, CancellationToken ct)
        {
            await _uow.OwnerUnits.RemoveByUnitAsync(r.UnitId, ct);
            await _uow.SaveChangesAsync(ct);

            await _mediator.Publish(new OwnerUnassignedFromUnitDomainEvent(r.UnitId), ct);
            return Result.Ok();
        }
    }
}
