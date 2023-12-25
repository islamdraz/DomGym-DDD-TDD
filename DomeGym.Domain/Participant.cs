using ErrorOr;

namespace DomeGym.Domain;

public class Participant
{
    private readonly Guid _id;
    private readonly Guid _userId;
    private readonly List<Guid> _sessionIds = new List<Guid>();

    private Schedule _schedule = Schedule.Empty();
    public Participant(Guid userId, Schedule? schedule = null, Guid? id = null)
    {
        _userId = userId;
        this._id = id ?? Guid.NewGuid();
        _schedule = schedule ?? Schedule.Empty();
    }

    public Guid Id
    {
        get
        {
            return this._id;

        }
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