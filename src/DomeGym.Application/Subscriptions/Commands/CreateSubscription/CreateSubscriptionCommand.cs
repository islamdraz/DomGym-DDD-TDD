using DomeGym.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand(SubscriptionType SubscriptionType, Guid AdminId)
    : IRequest<ErrorOr<Subscription>>;