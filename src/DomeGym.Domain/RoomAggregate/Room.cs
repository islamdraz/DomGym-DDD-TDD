using System.Globalization;
using DomeGym.Domain.Common;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private readonly Guid _id;
    private readonly Guid _gymId;
    private readonly int _maxDailySession;
    private readonly List<Guid> _sessionIds = new();

    private readonly Schedule _schedule = Schedule.Empty();
    public Room(int maxDailySession, Guid gymId, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxDailySession = maxDailySession;
        _gymId = gymId;
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (_sessionIds.Count >= _maxDailySession)
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
}