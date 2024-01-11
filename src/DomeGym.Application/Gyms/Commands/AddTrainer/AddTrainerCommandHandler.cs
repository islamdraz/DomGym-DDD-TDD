using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.TrainerAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{

    private readonly ISubscriptionRepository _subscriptionsRepository;
    private readonly IGymRepository _gymRepository;
    private readonly ITrainerRepository _trainerRepository;
    public AddTrainerCommandHandler(ISubscriptionRepository subscriptionsRepository, IGymRepository gymRepository, ITrainerRepository trainerRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymRepository = gymRepository;
        _trainerRepository = trainerRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);

        if (subscription is null)
        {
            return Error.NotFound(description: "Subscription not found");
        }

        if (!subscription.HasGym(request.GymId))
        {
            return Error.NotFound(description: "Gym not found");
        }

        var gym = await _gymRepository.GetByIdAsync(request.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        if (gym.HasTrainer(request.TrainerId))
        {
            return Error.Conflict("Trainer already in gym");
        }

        Trainer? trainer = await _trainerRepository.GetByIdAsync(request.TrainerId);

        if (trainer is null)
        {
            return Error.NotFound(description: "Trainer not found");
        }

        gym.AddTrainer(trainer);

        await _gymRepository.UpdateAsync(gym);

        return Result.Success;
    }
}
