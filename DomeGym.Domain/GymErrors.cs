using ErrorOr;

namespace DomeGym.Domain;

public static class GymErrors
{
    public static Error CannotHaveRoomMoreThanSubscription = Error.Validation(code: "Gym.CannotHaveRoomMoreThanSubscription",
                                                                              description: "cannot have rooms more than the subscription limit allowed");
}