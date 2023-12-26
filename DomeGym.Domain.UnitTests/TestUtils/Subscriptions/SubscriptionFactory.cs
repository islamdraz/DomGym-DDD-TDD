
namespace DomeGym.Domain.UnitTests;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(int maxGymAllowed, Guid? id = null)
    {
        return new Subscription(maxGymAllowed: maxGymAllowed, id ?? Constants.Subscription.Id);
    }
}