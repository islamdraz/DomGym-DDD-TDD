using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface ISubscriptionRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
    Task<Subscription?> GetByIdAsync(Guid id);
    Task<List<Subscription>> ListBySubscriptionIdAsync(Guid subscriptionId);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Subscription subscription);
    Task<List<Subscription>> ListAsync();

}