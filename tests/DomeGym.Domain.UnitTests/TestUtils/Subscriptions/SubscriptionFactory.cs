
using DomeGym.Domain.SubscriptionAggregate;

namespace DomeGym.Domain.UnitTests;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(SubscriptionType? subscriptionType = null, Guid? adminId = null, Guid? id = null)
    {
        return new Subscription(subscriptionType: subscriptionType ?? Constants.Subscription.SubscriptionType, adminId: adminId ?? Constants.Admin.Id, id
                                                                                                                                                       ?? Constants.Subscription.Id);
    }
}