using DomeGym.Application.Subscriptions.Commands.CreateSubscription;
using DomeGym.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[Route("subscriptions")]
public class SubscriptionsControllers : ApiController
{
    private readonly ISender _sender;
    public SubscriptionsControllers(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateSubscriptionRequest subscriptionRequest)
    {
        if (!Domain.SubscriptionAggregate.SubscriptionType.TryFromName(subscriptionRequest.SubscriptionType.ToString(), out var subscriptionType))
        {
            return Problem("Invalid subscription type", statusCode: StatusCodes.Status400BadRequest);
        }

        var command = new CreateSubscriptionCommand(subscriptionType, subscriptionRequest.AdminId);
        var result = await _sender.Send(command);

        return result.Match(subscription => Ok(new SubscriptionResponse(subscription.Id, ToDtoSubscription(subscription.SubscriptionType))), Problem);
    }

    private Contracts.Subscriptions.SubscriptionType ToDtoSubscription(Domain.SubscriptionAggregate.SubscriptionType subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(Domain.SubscriptionAggregate.SubscriptionType.Free) => Contracts.Subscriptions.SubscriptionType.Free,
            nameof(Domain.SubscriptionAggregate.SubscriptionType.Starter) => Contracts.Subscriptions.SubscriptionType.Starter,
            _ => throw new ArgumentOutOfRangeException(nameof(subscriptionType), subscriptionType, null)
        };
    }
}