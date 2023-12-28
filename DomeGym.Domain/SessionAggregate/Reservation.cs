using DomeGym.Domain.Common;

namespace DomeGym.Domain.SessionAggregate;

public class Reservation : Entity
{
    private readonly Guid _participantId;
    public Reservation(Guid participantId = default, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _participantId = participantId;
    }

    public Guid ParticipantId => _participantId;
}