using DomeGym.Domain.TrainerAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands.AddTrainer;

public record AddTrainerCommand(Guid SubscriptionId, Guid GymId, Guid TrainerId) : IRequest<ErrorOr<Success>>;