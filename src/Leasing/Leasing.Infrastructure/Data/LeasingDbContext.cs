using ApartmentManagement.SharedKernel.Entities; // base Entity with DomainEvents
using Leasing.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leasing.Infrastructure.Data
{
    public class LeasingDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public LeasingDbContext(DbContextOptions<LeasingDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Lease> Leases { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Leasing");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeasingDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Save to database first
            var result = await base.SaveChangesAsync(cancellationToken);

            // Gather domain events from tracked entities
            var entitiesWithEvents = ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            var domainEvents = entitiesWithEvents.SelectMany(e => e.DomainEvents).ToArray();

            foreach (var entity in entitiesWithEvents)
                entity.ClearDomainEvents();

            // Publish events via MediatR
            foreach (var @event in domainEvents)
                await _mediator.Publish(@event, cancellationToken);

            return result;
        }
    }
}
