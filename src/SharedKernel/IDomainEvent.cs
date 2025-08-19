using MediatR;

namespace ApartmentManagement.SharedKernel
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}

