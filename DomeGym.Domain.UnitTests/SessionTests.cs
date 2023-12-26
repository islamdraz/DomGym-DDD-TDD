using DomeGym.Domain;
using DomeGym.Domain.UnitTests.TestUtils.Participants;
using DomeGym.UnitTests.TestUtils.Services;
using DomeGym.UnitTests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.UnitTests
{
    public class SessionTests
    {
        [Fact]
        public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
        {
            // Arrang
            // create session
            var session = SessionFactory.CreateSession(maxPraticipants: 1);
            var participant1 = ParticipantFactory.CreateParticipant(Guid.NewGuid(), Guid.NewGuid());
            var participant2 = ParticipantFactory.CreateParticipant(Guid.NewGuid(), Guid.NewGuid());
            // create 2 participants

            // Act 
            // add participant 1
            // add participant 2

            // var action = () => session.ReserveSpot(participant2);
            var reserveParticipant1Result = session.ReserveSpot(participant1);
            var reserveParticipant2Result = session.ReserveSpot(participant2);
            // Assert
            // participant 2 reservation failed

            reserveParticipant1Result.IsError.Should().BeFalse();
            reserveParticipant2Result.IsError.Should().BeTrue();
            reserveParticipant2Result.FirstError.Should().Be(SessionErrors.CannotReserveParticipantsThanSessionCapacity);
        }

        [Fact]
        public void CancelReservation_WhenCancellationIsTooCloseToSession_ShouldFailCancellation()
        {
            // Arrang 
            // Create Session
            var session = SessionFactory.CreateSession(
                date: Constants.Session.Date,
                time: Constants.Session.Time
            );
            // Add Participant 
            var participant = ParticipantFactory.CreateParticipant();
            // Reserve a spot for the participant in the session
            session.ReserveSpot(participant);
            var cacellationDateTime = Constants.Session.Date.ToDateTime(TimeOnly.MinValue); // means same date but at 12 am
            // Act
            // Cancel Reservation 42 hours before the session


            // var action = ()=> session.CancelReservation(
            //     participant,
            //     new TestDateTimeProvider(fixedDateTime: cacellationDateTime));
            var sessionCancelReservationResult = session.CancelReservation(
                participant,
                new TestDateTimeProvider(fixedDateTime: cacellationDateTime));
            // Assert     
            // Reservation should fail
            sessionCancelReservationResult.IsError.Should().BeTrue();
            sessionCancelReservationResult.FirstError.Should().BeOneOf(SessionErrors.CannotReserveParticipantsThanSessionCapacity, SessionErrors.CannotCancelReservationTooCloseToSession);

        }
    }
}