using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Domain.UnitTests.TestConstants
{
    public static partial class Constants
    {
        public static class Subscription
        {
            public static readonly Guid Id = Guid.NewGuid();
            public static readonly SubscriptionType SubscriptionType = SubscriptionType.Free;
            public static int MaxDailySessionsFreeTier = 4;
            public static int MaxRoomsFreeTier = 4;
        }
    }
}