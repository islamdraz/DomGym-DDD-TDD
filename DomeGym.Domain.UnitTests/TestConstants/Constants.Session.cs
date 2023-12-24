using System.Reflection.Metadata;
using DomeGym.Domain;

namespace DomeGym.UnitTests.TestConstants
{
    public static partial class Constants
    {
        public static class Session
        {
            public static readonly Guid Id = Guid.NewGuid();
            public static readonly TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.UtcNow);
            public static readonly DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow);
            public static readonly TimeOnly EndTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(1));
            public static readonly TimeRange Time = new(TimeOnly.MinValue.AddHours(8), TimeOnly.MinValue.AddHours(9));
            public const int MaxParticipants = 1;

        }
    }
}