using ErrorOr;

namespace DomeGym.Domain;

public static class ParticipantErrors
{
    public static Error CannotHaveTwoOverlappingSessions = Error.Validation(
    code: "Subscription.CannotHaveTwoOverlappingSessions",
    description: "Cannot Have Two Overlapping Sessions");

}