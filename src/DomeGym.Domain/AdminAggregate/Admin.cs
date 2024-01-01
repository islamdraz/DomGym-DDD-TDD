using DomeGym.Domain.Common;

namespace DomeGym.Domain.AdminAggregate;
public class Admin : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid SubscriptionId;

    public Admin(Guid userId, Guid subscriptionId, Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
    }


    private Admin()
    {

    }
}