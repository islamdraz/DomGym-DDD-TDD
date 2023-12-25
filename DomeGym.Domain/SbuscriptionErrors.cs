using ErrorOr;

namespace DomeGym.Domain;

public static class SubscriptionErrors
{
    public static Error CannotAddGymMoreThanSubscriptionAllows = Error.Validation(
        code: "Participant.CannotAddGymMoreThanSubscriptionAllows",
        description: "Cannot Add Gym More Than Subscription Allows");

}

