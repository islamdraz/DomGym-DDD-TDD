using DomeGym.UnitTests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.UnitTests;



public class RoomTests
{
    [Fact]
    public void ScheduleSession_MoreThanSubscriptionAllows_ShouldFail()
    {
        // Given
        var room = RoomFactory.CreateRoom(1);

        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid());
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid());
        // When
        var scheduleSessionResult1 = room.ScheduleSession(session1);
        var scheduleSessionResult2 = room.ScheduleSession(session2);

        // Then

        scheduleSessionResult1.IsError.Should().BeFalse();
        scheduleSessionResult2.IsError.Should().BeTrue();

        scheduleSessionResult2.FirstError.Should().Be(RoomErrors.CannotReserveSessionsThanSubscriptionAllows);

    }
}