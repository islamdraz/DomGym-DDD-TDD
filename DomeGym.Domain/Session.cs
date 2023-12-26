using System.Runtime.InteropServices;
using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new();
    public DateOnly Date { get; }
    public TimeRange Time { get; }

    // private readonly Guid _roomid;
    private readonly int _maxParticipants;

    public Guid Id { get { return this._id; } }

    public Session(DateOnly date,
        TimeRange time,
        int maxParticipants,
        Guid trainerId,
        Guid? id = null)
    {
        this.Date = date;
        Time = time;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
        _id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider datetimeProvider)
    {
        if (IsTooCloseToSession(datetimeProvider.UtcNow))
        {

            return SessionErrors.CannotCancelReservationTooCloseToSession;
        }

        if (!_participantIds.Remove(participant.Id))
        {
            return SessionErrors.CannotCancelParticipantDoesnotExists;
        }

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minHours = 24;

        return (this.Date.ToDateTime(this.Time.Start) - utcNow).TotalHours < minHours;

    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_participantIds.Count() >= _maxParticipants)
        {
            return SessionErrors.CannotReserveParticipantsThanSessionCapacity;
        }
        _participantIds.Add(participant.Id);
        return Result.Success;
    }
}