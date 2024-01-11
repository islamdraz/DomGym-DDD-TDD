using DomeGym.Domain.Common;
using DomeGym.Domain.SubscriptionAggregate;
using ErrorOr;

namespace DomeGym.Domain.AdminAggregate;
public class Admin : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid? SubscriptionId { get; private set; }

    public Admin(Guid userId, Guid? subscriptionId = null, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    public ErrorOr<Success> SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue)
        {
            return Error.Conflict(description: "Admin already has an active subscription");
        }

        SubscriptionId = subscription.Id;

        return Result.Success;
    }

    private Admin()
    {

    }
}