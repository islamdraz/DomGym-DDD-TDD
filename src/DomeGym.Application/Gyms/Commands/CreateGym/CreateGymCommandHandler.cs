using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.SubscriptionAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands;

public class CreateGymCommandHanler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public CreateGymCommandHanler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }


    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        if (await _subscriptionRepository.GetByIdAsync(request.SubscriptionId) is not Subscription subscription)
        {
            return Error.NotFound("subscription not found ");
        }

        var gym = new Gym(name: request.Name, maxRooms: subscription.GetMaxGyms(), subscriptionId: subscription.Id);

        subscription.AddGym(gym);
        await _subscriptionRepository.UpdateAsync(subscription);

        return gym;

    }
}
