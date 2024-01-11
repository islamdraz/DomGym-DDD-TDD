using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Queries.ListGyms;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymRepository _gymRepository;

    public ListGymsQueryHandler(IGymRepository gymRepository, ISubscriptionRepository subscriptionRepository)
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery request, CancellationToken cancellationToken)
    {
        if (await _subscriptionRepository.GetByIdAsync(request.SubscriptionId) is not Subscription subscription)
        {
            return Error.NotFound("subscription not found");
        }

        return await _gymRepository.ListSubscriptionGymsAsync(request.SubscriptionId);
    }
}
