
using System.Reflection.Metadata;

namespace DomeGym.UnitTests.TestUtils.TestConstants;

public  static partial class Constants
{
    public static class Room 
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxDailySession = 1;
    }
}