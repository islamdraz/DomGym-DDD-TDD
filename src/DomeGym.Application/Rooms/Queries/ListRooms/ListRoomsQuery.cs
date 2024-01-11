using DomeGym.Domain.RoomAggregate;
using ErrorOr;
using MediatR;


namespace DomeGym.Application.Rooms.Queries;

public record ListRoomQuery(Guid GymId) : IRequest<ErrorOr<List<Room>>>;