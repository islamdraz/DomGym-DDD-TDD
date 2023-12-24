using DomeGym.Domain;
using DomeGym.UnitTests.TestConstants;

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