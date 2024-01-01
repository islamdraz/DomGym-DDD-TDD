using System.Globalization;
using DomeGym.Domain.Common;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    public Guid GymId { get; private set; }
    private int _maxDailySessions { get; }
    private readonly List<Guid> _sessionIds = new();

    private readonly Schedule _schedule = Schedule.Empty();

    public string Name { get; }
    public Room(string name, int maxDailySession, Guid gymId, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxDailySessions = maxDailySession;
        GymId = gymId;
        Name = name;
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Count >= _maxDailySessions)
        {
            return RoomErrors.CannotReserveSessionsThanSubscriptionAllows;
        }
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict("session already exists");

        var addEventResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (addEventResult.IsError)
        {
            return RoomErrors.CannotHaveTwoOverlappingSessions;
        }

        _sessionIds.Add(session.Id);

        return Result.Success;
    }

    private Room() { }
}