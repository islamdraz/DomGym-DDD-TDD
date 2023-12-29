using ErrorOr;

namespace DomeGym.Domain.ParticipantAggregate;

public static class ParticipantErrors
{
    public static Error CannotHaveTwoOverlappingSessions = Error.Validation(
    code: "Subscription.CannotHaveTwoOverlappingSessions",
    description: "Cannot Have Two Overlapping Sessions");

}