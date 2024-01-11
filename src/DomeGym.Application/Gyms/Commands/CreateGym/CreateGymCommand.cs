using DomeGym.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Commands;

public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;