using DomeGym.Domain.SubscriptionAggregate;
using DomeGym.UnitTests.Gyms;
using FluentAssertions;

namespace DomeGym.Domain.UnitTests.SubscriptionAggregate
{

    public class SubscriptionTest
    {
        [Fact]
        public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
        {
            // Given or Arrang
            var subscription = SubscriptionFactory.CreateSubscription(1);
            var gym1 = GymFactory.CreateGym(1, Guid.NewGuid());
            var gym2 = GymFactory.CreateGym(1, Guid.NewGuid());
            // When or Act 

            var addGymToSubscriptionResult1 = subscription.AddGym(gym1);
            var addGymToSubscriptionResult2 = subscription.AddGym(gym2);
            // Then or Assert
            addGymToSubscriptionResult1.IsError.Should().BeFalse();
            addGymToSubscriptionResult2.IsError.Should().BeTrue();
            addGymToSubscriptionResult2.FirstError.Should().Be(SubscriptionErrors.CannotAddGymMoreThanSubscriptionAllows);

        }
    }
}