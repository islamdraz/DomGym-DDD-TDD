using System.Linq;
using ErrorOr;

namespace DomeGym.Domain;

public class Subscription
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _gymIds = new();
    public Guid Id { get { return this._id; } }
    private readonly int _maxGymAllowed;
    public Subscription(int maxGymAllowed, Guid? id)
    {
        _maxGymAllowed = maxGymAllowed;
        this._id = id ?? Guid.NewGuid();
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