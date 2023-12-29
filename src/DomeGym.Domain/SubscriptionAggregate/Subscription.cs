using System.Linq;
using DomeGym.Domain.Common;
using DomeGym.Domain.GymAggregate;
using ErrorOr;

namespace DomeGym.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private readonly Guid _userId;
    private readonly List<Guid> _gymIds = new();
    private readonly int _maxGymAllowed;
    public Subscription(int maxGymAllowed, Guid userId, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxGymAllowed = maxGymAllowed;
        _userId = userId;
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Count >= _maxGymAllowed)
        {
            return SubscriptionErrors.CannotAddGymMoreThanSubscriptionAllows;
        }
        if (_gymIds.Contains(gym.Id))
            return Error.Conflict("gym is already exists in the list");

        _gymIds.Add(gym.Id);

        return Result.Success;

    }
}