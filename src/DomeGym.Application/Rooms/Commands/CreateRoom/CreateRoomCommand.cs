using DomeGym.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(Guid gymId, string roomName) : IRequest<ErrorOr<Room>>;