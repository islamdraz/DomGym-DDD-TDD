
namespace DomeGym.Domain.UnitTests
{

    public static class RoomFactory
    {

        public static Room CreateRoom(int? maxDailySession = null, Guid? gymId = null, Guid? roomId = null)
        {
            return new Room(maxDailySession: maxDailySession ?? Constants.Room.MaxDailySession, gymId ?? Constants.Gym.Id, roomId ?? Constants.Room.Id);
        }
    }
}