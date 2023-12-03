using ErrorOr;

namespace DomeGym.Domain;

public static class SessionErrors
{
    public static Error CannotCancelReservationTooCloseToSession = Error.Validation(
        code: "Session.CannotCancelReservationTooCloseToSession",
        description: "cannot cancel reservation too close to session start time");
    public static Error CannotCancelParticipantDoesnotExists = Error.Validation(
        code: "Session.CannotCancelParticipantDoesnotExists",
        description: "Cannot Cancel Participant Doesn't exists in the session attendies");
        
    public static Error CannotReserveParticipantsThanSessionCapacity = Error.Validation(
        code: "Session.CannotReserveParticipantsThanSessionCapacity",
        description: "Cannot Reserve Participants than session capacity");
}

