
using DomeGym.Domain.GymAggregate;


namespace DomeGym.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);
    Task<Gym?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<List<Gym>> ListSubscriptionGymsAsync(Guid subscriptionId);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(Gym gym);

}