using DomeGym.Domain;
using DomeGym.UnitTests.TestUtils.TestConstants;

namespace DomeGym.UnitTests.TestUtils.Trainers;

public static class TrainerFactory
{
    public static Trainer CreateTrainer(int maxPraticipants)
    {
        return new Trainer();
    }
}