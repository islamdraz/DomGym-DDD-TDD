using DomeGym.Domain.GymAggregate;

namespace DomeGym.UnitTests.Gyms
{
    public static class GymFactory
    {
        public static Gym CreateGym(int maxRooms, Guid? id = null)
        {
            return new Gym(maxRooms, id ?? Constants.Gym.Id);
        }
    }
}