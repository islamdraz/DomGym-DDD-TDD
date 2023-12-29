using DomeGym.Domain.Common;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.ParticipantAggregate;

public class Participant : AggregateRoot
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new List<Guid>();

    private Schedule _schedule = Schedule.Empty();
    public Participant(Guid userId, Schedule? schedule = null, Guid? id = null) : base(id: id ?? Guid.NewGuid())
    {
        _userId = userId;
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
}