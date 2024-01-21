using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.SessionAggregate;

namespace DomeGym.UnitTests.TestUtils.Sessions
{
    public static class SessionFactory
    {
        public static Session CreateSession(
            string? name = null,
            DateOnly? date = null,
            TimeRange? time = null,
            int maxPraticipants = Constants.Session.MaxParticipants,
            Guid? roomId = null,
            Guid? trainerId = null,
            List<SessionCategory>? categories = null,
            Guid? id = null)
        {
            return new Session(
                                name: name ?? Constants.Session.Name,
                                date: date ?? Constants.Session.Date,
                                time: time ?? Constants.Session.Time,
                                maxParticipants: maxPraticipants,
                                roomId: roomId ?? Constants.Room.Id,
                                trainerId: trainerId ?? Constants.Trainer.Id,
                                categories: categories ?? Constants.Session.Categories,
                                id: id ?? Constants.Session.Id
                                 );
        }
    }
}