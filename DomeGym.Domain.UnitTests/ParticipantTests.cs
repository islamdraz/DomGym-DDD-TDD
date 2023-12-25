using DomeGym.Domain.UnitTests.TestUtils;
using DomeGym.Domain.UnitTests.TestUtils.Participants;
using DomeGym.UnitTests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domain.UnitTests;

public class ParticipantTests
{
    [Theory]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 1, 2)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionTimeOverlapWithOtherSession_ShouldFail(int startHourSession1, int endHourSession1, int startHourSession2, int endHourSession2)
    {

        // Given
        var participant = ParticipantFactory.CreateParticipant();
        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid(), date: Constants.Session.Date, time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1));
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid(), date: Constants.Session.Date, time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2));
        // When
        // When

        var scheduleSession1Result = participant.AddSessionToSchedule(session1);
        var scheduleSession2Result = participant.AddSessionToSchedule(session2);

        // Then
        scheduleSession1Result.IsError.Should().BeFalse();
        scheduleSession2Result.IsError.Should().BeTrue();

        scheduleSession2Result.FirstError.Should().Be(ParticipantErrors.CannotHaveTwoOverlappingSessions);

    }
}