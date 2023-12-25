using DomeGym.Domain;
using DomeGym.Domain.UnitTests.TestConstants;

namespace DomeGym.UnitTests.TestUtils.Trainers
{
    public static class TrainerFactory
    {
        public static Trainer CreateTrainer(Guid? userId = null, Guid? id = null)
        {
            return new Trainer(userId: userId ?? Constants.User.Id,
                               id: id ?? Constants.Trainer.Id);

        }
    }
}