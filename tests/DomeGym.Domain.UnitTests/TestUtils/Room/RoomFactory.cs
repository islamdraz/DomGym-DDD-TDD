
using DomeGym.Domain.RoomAggregate;

namespace DomeGym.Domain.UnitTests
{

    public static class RoomFactory
    {

        public static Room CreateRoom(string name = null, int? maxDailySession = null, Guid? gymId = null, Guid? roomId = null)
        {
            return new Room(name: name ?? Constants.Room.Name, maxDailySession: maxDailySession ?? Constants.Room.MaxDailySession, gymId ?? Constants.Gym.Id, roomId ?? Constants.Room.Id);
        }
    }
}