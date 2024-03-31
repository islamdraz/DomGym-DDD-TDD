using DomeGym.Application.Subscriptions.Commands.CreateSubscription;
using DomeGym.Application.Subscriptions.Queries.ListSubscriptions;
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
        var x = subscriptionRequest.SubscriptionType.ToString();
        if (Domain.SubscriptionAggregate.SubscriptionType.TryFromName(subscriptionRequest.SubscriptionType.ToString(), out var subscriptionType))
        {
            return Problem("Invalid subscription type", statusCode: StatusCodes.Status400BadRequest);
        }

        var command = new CreateSubscriptionCommand(subscriptionType, subscriptionRequest.AdminId);
        var result = await _sender.Send(command);

        return result.Match(subscription => Ok(new SubscriptionResponse(subscription.Id, ToDtoSubscription(subscription.SubscriptionType))), Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListSubscription()
    {
        var command = new ListSubscriptionsQuery();
        var result = await _sender.Send(command);

        return result.Match(subscriptions => Ok(subscriptions.Select(x => new SubscriptionResponse(Id: x.Id, ToDtoSubscription(x.SubscriptionType)))), Problem);
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