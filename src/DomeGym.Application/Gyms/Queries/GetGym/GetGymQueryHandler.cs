using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Queries.GetGym;

public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{

    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IGymRepository _gymRepository;

    public GetGymQueryHandler(ISubscriptionRepository subscriptionRepository, IGymRepository gymRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _gymRepository = gymRepository;
    }

    public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {
        if (await _subscriptionRepository.GetByIdAsync(request.SubscriptionId) is not Subscription subscription)
        {
            return Error.NotFound("Subscription is not exists");
        }


        if (subscription.HasGym(request.GymId))
        {
            return Error.NotFound("Gym is not exists");
        }

        if (await _gymRepository.GetByIdAsync(request.GymId) is not Gym gym)
        {
            return Error.NotFound(description: "Gym not found");
        }

        return gym;

    }
}
