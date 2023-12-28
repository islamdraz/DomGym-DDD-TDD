using DomeGym.Domain.Common;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

namespace DomeGym.Domain.TrainerAggregate;

public class Trainer : AggregateRoot
{
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new();
    public Schedule _schedule = Schedule.Empty();

    public Trainer(Guid userId, Schedule? schedule = null, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _schedule = schedule ?? Schedule.Empty();
        _userId = userId;
    }

    public ErrorOr<Success> AddSessionToSchedule(Session session)
    {
        if (_sessionIds.Contains(session.Id))
            return Error.Conflict("session is already added to trainer schedule ");

        var bookTrainerTimeSlotResult = _schedule.BookTimeSlot(session.Date, session.Time);
        if (bookTrainerTimeSlotResult.IsError && bookTrainerTimeSlotResult.FirstError.Type == ErrorType.Conflict)
            return TrainerError.CannotHaveTwoMoreOverlappingSessions;

        _sessionIds.Add(session.Id);
        return Result.Success;
    }
}