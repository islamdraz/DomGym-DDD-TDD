using System.Runtime.InteropServices;
using ErrorOr;

namespace DomeGym.Domain;

public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new();
    private readonly DateOnly _date;
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;

    // private readonly Guid _roomid;
    private readonly int _maxParticipants;

    public Session(DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        int maxParticipants,
        Guid trainerId,
        Guid? id = null)
    {
        this._date = date;
        this._startTime = startTime;
        this._endTime = endTime;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
        _id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider datetimeProvider)
    {
        if(IsTooCloseToSession(datetimeProvider.UtcNow))
        {

            return SessionErrors.CannotCancelReservationTooCloseToSession;
        }

        if(!_participantIds.Remove(participant.Id))
        {
            return SessionErrors.CannotCancelParticipantDoesnotExists;
        }

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
         const int minHours = 24;

         return (this._date.ToDateTime(this._startTime) - utcNow).TotalHours < minHours;

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