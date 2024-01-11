using DomeGym.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Rooms.Queries;

public record GetRoomQuery(Guid GymId, Guid ID) : IRequest<ErrorOr<Room>>;