using Ardalis.SmartEnum;

namespace DomeGym.Domain.SubscriptionAggregate;

public class SubscriptionType : SmartEnum<SubscriptionType>
{
    public static SubscriptionType Free = new(nameof(Free), 0);
    public static SubscriptionType Starter = new(nameof(Starter), 1);
    public static SubscriptionType Pro = new(nameof(Pro), 2);

    public SubscriptionType(string name, int value) : base(name, value)
    {
    }
}