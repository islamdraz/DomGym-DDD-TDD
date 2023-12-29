using System.Runtime.InteropServices;
using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.ParticipantAggregate;
using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private readonly Guid _trainerId;
    private readonly List<Reservation> _reservations = new();
    public DateOnly Date { get; }
    public TimeRange Time { get; }

    // private readonly Guid _roomid;
    private readonly int _maxParticipants;

    public Session(DateOnly date,
        TimeRange time,
        int maxParticipants,
        Guid trainerId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Date = date;
        Time = time;
        _maxParticipants = maxParticipants;
        _trainerId = trainerId;
    }

    public ErrorOr<Success> CancelReservation(Participant participant, IDateTimeProvider datetimeProvider)
    {
        if (IsTooCloseToSession(datetimeProvider.UtcNow))
        {

            return SessionErrors.CannotCancelReservationTooCloseToSession;
        }
        var reservation = _reservations.Find(reservation => reservation.ParticipantId == participant.Id);
        if (reservation is null)
        {
            return SessionErrors.CannotCancelParticipantDoesnotExists;
        }

        _reservations.Remove(reservation);

        return Result.Success;
    }

    private bool IsTooCloseToSession(DateTime utcNow)
    {
        const int minHours = 24;

        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < minHours;

    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_reservations.Count() >= _maxParticipants)
        {
            return SessionErrors.CannotReserveParticipantsThanSessionCapacity;
        }
        if (_reservations.Any(reservation => reservation.ParticipantId == participant.Id))
        {
            return Error.Conflict("Partcipant already exists in the session");
        }
        var reservation = new Reservation(participantId: participant.Id);
        _reservations.Add(reservation);
        return Result.Success;
    }
}