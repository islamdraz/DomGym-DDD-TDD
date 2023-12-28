using ErrorOr;

namespace DomeGym.Domain.TrainerAggregate;

public static class TrainerError
{

    public static Error CannotHaveTwoMoreOverlappingSessions = Error.Validation(code: "Trainer.CannotHaveTwoMoreOverlappingSessions", description: " Cannot Have Two More Overlapping Sessions");
}