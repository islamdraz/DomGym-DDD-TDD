using ErrorOr;

namespace DomeGym.Domain;

public class Room
{
    private readonly Guid _id;
    private readonly Guid _gymId;
    private readonly int _maxDailySession;
    private readonly List<Guid> _sessionIds = new();
    public Room(int maxDailySession, Guid gymId, Guid? id = null)
    {
        this._maxDailySession = maxDailySession;
        gymId = gymId;
        this._id = id ?? Guid.NewGuid();
    }
    public Guid Id
    {
        get
        {
            return this._id;
        }
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Count >= this._maxDailySession)
        {
            return RoomErrors.CannotReserveSessionsThanSubscriptionAllows;
        }
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict("session already exists");

        _sessionIds.Add(session.Id);

        return Result.Success;
    }
}