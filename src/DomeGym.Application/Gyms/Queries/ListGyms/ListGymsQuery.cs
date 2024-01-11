using DomeGym.Domain.GymAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Gyms.Queries.ListGyms;


public record ListGymsQuery(Guid SubscriptionId) : IRequest<ErrorOr<List<Gym>>>;