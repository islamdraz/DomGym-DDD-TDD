using ErrorOr;

namespace DomeGym.Domain;

public static class RoomErrors
{
    public static Error CannotReserveSessionsThanSubscriptionAllows = Error.Validation(
        code: "Room.CannotReserveSessionsThanSubscriptionAllows",
        description: "Cannot Reserve Sessions Than Subscription Allows");



}

