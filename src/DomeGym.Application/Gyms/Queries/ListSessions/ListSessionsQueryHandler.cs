using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SessionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Queries.ListSessions;

public class ListSessionsQueryHandler : IRequestHandler<ListSessionsQuery, ErrorOr<List<Session>>>
{
    private readonly ISubscriptionsRepository _subscriptionRepository;
    private readonly IGymsRepository _gymRepository;
    private readonly ISessionsRepository _sessionRepository;

    public ListSessionsQueryHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionsRepository, ISessionsRepository sessionsRepository)
    {
        _gymRepository = gymsRepository;
        _subscriptionRepository = subscriptionsRepository;
        _sessionRepository = sessionsRepository;
    }

    public async Task<ErrorOr<List<Session>>> Handle(ListSessionsQuery query, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetByIdAsync(query.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        if (!subscription.HasGym(query.GymId))
        {
            return Error.NotFound(description: "Gym not found");
        }

        if (!await _gymRepository.ExistsAsync(query.GymId))
        {
            return Error.NotFound(description: "Gym not found");
        }

        return await _sessionRepository.ListByGymIdAsync(query.GymId, query.StartDateTime, query.EndDateTime, query.Categories);
    }
}
