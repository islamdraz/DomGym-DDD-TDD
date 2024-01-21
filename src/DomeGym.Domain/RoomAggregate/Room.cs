using System.Globalization;
using DomeGym.Domain.Common;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    public Guid GymId { get; private set; }
    private int _maxDailySessions { get; }
    private readonly Dictionary<DateOnly, List<Guid>> _sessionIdsByDate = new();

    private readonly Schedule _schedule = Schedule.Empty();

    public string Name { get; }
    public IReadOnlyList<Guid> SessionIds => _sessionIdsByDate.Values.SelectMany(x => x).ToList();
    public Room(string name, int maxDailySession, Guid gymId, Schedule? schedule = null, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxDailySessions = maxDailySession;
        GymId = gymId;
        Name = name;
        _schedule = schedule ?? Schedule.Empty();
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        if (session.Date < DateOnly.FromDateTime(DateTime.UtcNow))
        {
            return Error.Conflict("cannot schedule session in the past");
        }

        if (!_sessionIdsByDate.ContainsKey(session.Date))
        {
            _sessionIdsByDate.Add(session.Date, new List<Guid>());
        }

        var todaySessions = _sessionIdsByDate[session.Date];

        if (todaySessions.Count >= _maxDailySessions)
        {
            return RoomErrors.CannotReserveSessionsThanSubscriptionAllows;
        }


        var addEventResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (addEventResult.IsError)
        {
            return RoomErrors.CannotHaveTwoOverlappingSessions;
        }

        todaySessions.Add(session.Id);

        return Result.Success;
    }

    public bool HasSession(Guid sessionId)
    {
        return SessionIds.Contains(sessionId);
    }

    private Room() { }
}