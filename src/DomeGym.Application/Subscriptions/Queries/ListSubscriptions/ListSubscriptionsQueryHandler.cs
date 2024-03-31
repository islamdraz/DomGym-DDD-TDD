using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Subscriptions.Queries.ListSubscriptions;

public class ListSubscriptionsQueryHandler : IRequestHandler<ListSubscriptionsQuery, ErrorOr<List<Subscription>>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public ListSubscriptionsQueryHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<List<Subscription>>> Handle(ListSubscriptionsQuery request, CancellationToken cancellationToken)
    {
        return await _subscriptionsRepository.ListAsync();
    }
}
