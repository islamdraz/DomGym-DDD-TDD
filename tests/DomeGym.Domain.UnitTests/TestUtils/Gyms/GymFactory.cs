using System.Reflection.Metadata;
using DomeGym.Domain.GymAggregate;

namespace DomeGym.UnitTests.Gyms
{
    public static class GymFactory
    {
        public static Gym CreateGym(string? name = null, int? maxRooms = null, Guid? id = null)
        {
            return new Gym(name: name ?? Constants.Gym.Name, maxRooms ?? int.MaxValue, id ?? Constants.Gym.Id);
        }
    }
}