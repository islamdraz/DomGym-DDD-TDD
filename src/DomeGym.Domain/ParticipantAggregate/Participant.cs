using DomeGym.Domain.Common;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();
    public Guid UserId { get; }
    public IReadOnlyList<Guid> SessionIds => _sessionIds;
    public Participant(Guid userId,
                       Schedule? schedule = null,
                       Guid? id = null) : base(id: id ?? Guid.NewGuid())
    {
        UserId = userId;
        _schedule = schedule ?? Schedule.Empty();
    }


    public ErrorOr<Success> AddSessionToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict("session is already added to trainer schedule ");

        var bookTrainerTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookTrainerTimeSlotResult.IsError && bookTrainerTimeSlotResult.FirstError.Type == ErrorType.Conflict)
            return ParticipantErrors.CannotHaveTwoOverlappingSessions;

        _sessionIds.Add(session.Id);
        return Result.Success;
    }

    public bool HasReservationForSession(Guid id)
    {
        return _sessionIds.Contains(id);
    }

    public bool IsTimeSlotFree(DateOnly date, TimeRange time)
    {
        return _schedule.CanBookTimeSlot(date, time);
    }

    private Participant() { }
}