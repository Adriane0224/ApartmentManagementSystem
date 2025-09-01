using ApartmentManagement.Contracts.Services;
using AutoMapper;
using FluentResults;
using Leasing.Application.Commands;
using Leasing.Application.Errors;
using Leasing.Application.Response;
using Leasing.Domain.Entities;
using Leasing.Domain.Repositories;
using Leasing.Domain.ValueObject;
using MediatR;
using static Leasing.Application.Commands.CreateLeaseCommands;
using static Leasing.Application.Commands.TerminateLeaseCommands;
using static Leasing.Application.Commands.RenewLeaseCommands;   // 👈 add this

namespace Leasing.Application.CommandHandler
{
    public class LeaseCommands :
        ILeaseCommands,
        IRequestHandler<CreateLeaseCommand, Result<LeaseResponse>>,
        IRequestHandler<TerminateLeaseCommand, Result>,
        IRequestHandler<RenewLeaseCommand, Result<LeaseResponse>>   // 👈 add this
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeaseRepository _leases;
        private readonly IMapper _mapper;

        private readonly IApartmentReadPort? _apartments;
        private readonly ITenantReadPort? _tenants;
        private readonly IEventBus _eventBus;

        public LeaseCommands(
            IUnitOfWork unitOfWork,
            ILeaseRepository leases,
            IMapper mapper,
            IEventBus eventBus,
            IApartmentReadPort? apartments = null,
            ITenantReadPort? tenants = null)
        {
            _unitOfWork = unitOfWork;
            _leases = leases;
            _mapper = mapper;
            _eventBus = eventBus;
            _apartments = apartments;
            _tenants = tenants;
        }

        public async Task<Result<LeaseResponse>> Handle(CreateLeaseCommand r, CancellationToken ct)
            => await CreateAsync(r.ApartmentId, r.TenantId, r.StartDate, r.EndDate, r.MonthlyRent, r.SecurityDeposit, ct);

        public async Task<Result> Handle(TerminateLeaseCommand r, CancellationToken ct)
            => await TerminateAsync(r.LeaseId, r.TerminationDate, ct);

        public async Task<Result<LeaseResponse>> Handle(RenewLeaseCommand r, CancellationToken ct)   // 👈 add this
            => await RenewAsync(r.LeaseId, r.NewEndDate, r.NewMonthlyRent, ct);

        public async Task<Result<LeaseResponse>> CreateAsync(
            Guid apartmentUnitId, Guid tenantId, DateOnly start, DateOnly end,
            decimal monthlyRent, decimal deposit, CancellationToken ct)
        {
            // Optional cross-context checks
            if (_apartments is not null && !await _apartments.IsAvailableAsync(apartmentUnitId, start, end, ct))
                return Result.Fail(LeaseError.ApartmentNotAvailable);

            if (_tenants is not null && !await _tenants.ExistsAsync(tenantId, ct))
                return Result.Fail(LeaseError.InvalidTenant);

            if (await _leases.ExistsOverlapAsync(apartmentUnitId, start, end, ct))
                return Result.Fail(LeaseError.Overlap);

            var lease = Lease.Activate(apartmentUnitId, tenantId, start, end, monthlyRent, deposit);

            await _leases.AddAsync(lease, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            // Integration event so Property flips to Occupied
            await _eventBus.PublishAsync(new LeaseActivatedIntegrationEvent(apartmentUnitId), ct);

            return Result.Ok(_mapper.Map<LeaseResponse>(lease));
        }

        public async Task<Result> TerminateAsync(Guid leaseId, DateOnly terminationDate, CancellationToken ct)
        {
            var lease = await _leases.GetByIdForUpdateAsync(new LeaseId(leaseId), ct);
            if (lease is null) return Result.Fail(LeaseError.NotFound);

            lease.Terminate(terminationDate);

            await _unitOfWork.SaveChangesAsync(ct);

            // Integration event so Property flips to Available
            await _eventBus.PublishAsync(new LeaseTerminatedIntegrationEvent(lease.ApartmentId), ct);

            return Result.Ok();
        }

        // 👇 New: renew logic
        private async Task<Result<LeaseResponse>> RenewAsync(Guid leaseId, DateOnly newEndDate, decimal? newMonthlyRent, CancellationToken ct)
        {
            var lease = await _leases.GetByIdForUpdateAsync(new LeaseId(leaseId), ct);
            if (lease is null) return Result.Fail(LeaseError.NotFound);

            lease.Renew(newEndDate, newMonthlyRent);
            await _unitOfWork.SaveChangesAsync(ct);

            return Result.Ok(_mapper.Map<LeaseResponse>(lease));
        }
    }
}
