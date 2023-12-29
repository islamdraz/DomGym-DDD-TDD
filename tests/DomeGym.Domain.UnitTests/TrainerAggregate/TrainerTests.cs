using DomeGym.Domain.TrainerAggregate;
using DomeGym.Domain.UnitTests.TestUtils;
using DomeGym.UnitTests.TestUtils.Sessions;
using DomeGym.UnitTests.TestUtils.Trainers;
using FluentAssertions;


namespace DomeGym.Domain.UnitTests.TrainerAggregate;

public class TainerTests
{
    [Theory]
    [InlineData(1, 2, 1, 2)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 2, 0, 3)]
    public void AddSessionToSchedule_WhenTrainerHasOverlabSession_ShouldFail(int startHourSession1,
                                                                            int endHourSession1,
                                                                            int startHourSession2,
                                                                            int endHourSession2)
    {
        // Given
        var trainer = TrainerFactory.CreateTrainer();

        var session1 = SessionFactory.CreateSession(id: Guid.NewGuid(), date: Constants.Session.Date, time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1));
        var session2 = SessionFactory.CreateSession(id: Guid.NewGuid(), date: Constants.Session.Date, time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2));
        // When

        var addSession1ToScheduleResult = trainer.AddSessionToSchedule(session1);

        var addSession2ToScheduleResult = trainer.AddSessionToSchedule(session2);

        // Then


        addSession1ToScheduleResult.IsError.Should().BeFalse();
        addSession2ToScheduleResult.IsError.Should().BeTrue();
        addSession2ToScheduleResult.FirstError.Should().Be(TrainerError.CannotHaveTwoMoreOverlappingSessions);


    }
}
