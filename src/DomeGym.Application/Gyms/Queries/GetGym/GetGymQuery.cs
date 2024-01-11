using DomeGym.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Queries.GetGym;

public record GetGymQuery(Guid SubscriptionId, Guid GymId) : IRequest<ErrorOr<Gym>>;