using System.Reflection.Metadata;

namespace DomeGym.UnitTests.TestUtils.TestConstants;

public  static partial class Constants
{
    public static class Session 
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static DateOnly Date = DateOnly.FromDateTime(DateTime.UtcNow) ;
        public static TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.UtcNow);
        public static TimeOnly EndTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(1));
        public   const int MaxParticipants = 1;
    }
}