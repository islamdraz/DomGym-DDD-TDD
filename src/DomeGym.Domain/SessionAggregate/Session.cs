using DomeGym.Domain.Common;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.ParticipantAggregate;
using ErrorOr;

namespace DomeGym.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private List<Reservation> _reservations { get; } = new();
    private readonly List<SessionCategory> _categories = new();

    public int NumParticipants => _reservations.Count();
    public DateOnly Date { get; }
    public TimeRange Time { get; }
    public string Name { get; } = null!;
    public string Description { get; } = null!;
    public int MaxParticipants { get; }

    public Guid RoomId { get; }
    public Guid TrainerId { get; }

    public IReadOnlyList<SessionCategory> Categories => _categories;

    // private readonly Guid _roomid;
    private readonly int _maxParticipants;

    public Session(
        string name,
        DateOnly date,
        TimeRange time,
        int maxParticipants,
        Guid roomId,
        Guid trainerId,
        List<SessionCategory> categories,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Date = date;
        Time = time;
        _maxParticipants = maxParticipants;
        RoomId = roomId;
        TrainerId = trainerId;
        _categories = categories;
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

    public bool HasReservationForParticipant(Guid participantId)
    {
        return _reservations.Any(x => x.ParticipantId == participantId);
    }

    private Session() { }
}