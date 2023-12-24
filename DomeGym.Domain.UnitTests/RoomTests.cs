using System.Reflection.Metadata;
using DomeGym.Domain.UnitTests.TestUtils;
using DomeGym.UnitTests.TestConstants;
using DomeGym.UnitTests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.UnitTests
{


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

        [Theory]
        [InlineData(1, 2, 1, 2)]
        [InlineData(1, 3, 2, 4)]
        [InlineData(1, 3, 2, 3)]
        [InlineData(1, 2, 0, 2)]
        public void ScheduleSession_OverlabWithRegisteredSession_ShouldFail(int startHourSession1,
                                                                            int endHourSession1,
                                                                            int startHourSession2,
                                                                            int endHourSession2)
        {
            // Given
            var room = RoomFactory.CreateRoom(maxDailySession: 2);

            var session1 = SessionFactory.CreateSession(id: Guid.NewGuid(), date: Constants.Session.Date, time: TimeRangeFactory.CreateFromHours(1, 2));
            var session2 = SessionFactory.CreateSession(id: Guid.NewGuid(), date: Constants.Session.Date, time: TimeRangeFactory.CreateFromHours(0, 2));
            // When

            var schedualSession1Result = room.ScheduleSession(session1);
            var schedualSession2Result = room.ScheduleSession(session2);
            // Then

            schedualSession1Result.IsError.Should().BeFalse();
            schedualSession2Result.IsError.Should().BeTrue();

            schedualSession2Result.FirstError.Should().Be(RoomErrors.CannotHaveTwoOverlappingSessions);


        }



    }
}