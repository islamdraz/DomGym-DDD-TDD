using DomeGym.Domain;
using DomeGym.Domain.UnitTests;
using DomeGym.UnitTests.Gyms;
using FluentAssertions;

namespace DomeGym.UnitTests
{
    public class GymTests
    {
        [Fact]
        public void AddRoom_MoreThanSubscriptionAllow_FailAddRoom()
        {
            // Given
            // Create Gym
            var gym = GymFactory.CreateGym(1);
            var room1 = RoomFactory.CreateRoom(maxDailySession: 1, gymId: gym.Id, roomId: Guid.NewGuid());
            var room2 = RoomFactory.CreateRoom(maxDailySession: 1, gymId: gym.Id, roomId: Guid.NewGuid());
            // When
            // Add Room

            var room1Result = gym.AddRoom(room1);
            var room2Result = gym.AddRoom(room2);

            // Then
            // fail to add room

            room1Result.IsError.Should().BeFalse();
            room2Result.IsError.Should().BeTrue();
            room2Result.FirstError.Should().Be(GymErrors.CannotHaveRoomMoreThanSubscription);

        }
    }
}