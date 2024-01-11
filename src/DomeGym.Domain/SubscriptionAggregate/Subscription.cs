using System.Linq;
using DomeGym.Domain.Common;
using DomeGym.Domain.GymAggregate;
using ErrorOr;

namespace DomeGym.Domain.SubscriptionAggregate;

public class Subscription : AggregateRoot
{
    private Guid _adminId { get; }
    private List<Guid> _gymIds { get; } = new();
    private int _maxGyms { get; }

    public SubscriptionType SubscriptionType { get; }
    public Subscription(SubscriptionType subscriptionType, Guid adminId, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxGyms = GetMaxGyms();
        _adminId = adminId;
        SubscriptionType = subscriptionType;
    }

    public int GetMaxGyms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 1,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };


    public int GetMaxDailySessions() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 4,
        nameof(SubscriptionType.Starter) => int.MaxValue,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        _ => throw new InvalidOperationException()
    };

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Count >= _maxGyms)
        {
            return SubscriptionErrors.CannotAddGymMoreThanSubscriptionAllows;
        }
        if (_gymIds.Contains(gym.Id))
            return Error.Conflict("gym is already exists in the list");

        _gymIds.Add(gym.Id);

        return Result.Success;

    }

    public bool HasGym(Guid gymId)
    {
        return _gymIds.Contains(gymId);
    }

    private Subscription() { }
}