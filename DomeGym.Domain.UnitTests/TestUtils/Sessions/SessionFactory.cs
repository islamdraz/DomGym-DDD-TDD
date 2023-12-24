using DomeGym.Domain;
using DomeGym.UnitTests.TestConstants;

namespace DomeGym.UnitTests.TestUtils.Sessions
{
    public static class SessionFactory
    {
        public static Session CreateSession(
            DateOnly? date = null,
            TimeRange? time = null,
            int maxPraticipants = Constants.Session.MaxParticipants,
            Guid? id = null)
        {
            return new Session(
                                date: date ?? Constants.Session.Date,
                                time: time ?? Constants.Session.Time,
                                maxParticipants: maxPraticipants,
                                trainerId: Constants.Trainer.Id,
                                id: id ?? Constants.Session.Id
                                 );
        }
    }
}